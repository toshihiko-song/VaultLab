using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using VaultLab.Application.Abstractions;


namespace VaultLab.Infrastructure.Documents
{
    public class ExcelDocumentTextExtractor : IDocumentTextExtractor
    {
        public Task<string> ExtractAsync(Stream content, string contentType, CancellationToken cancellationToken = default)
        {
            using var document = SpreadsheetDocument.Open(content, false);

            var workbookPart = document.WorkbookPart;

            if (workbookPart is null)
                return Task.FromResult(string.Empty);

            var workbook = workbookPart.Workbook;

            if (workbook is null)
                return Task.FromResult(string.Empty);

            var sheets = workbook
                .Sheets?
                .Elements<Sheet>()
                ?? [];

            var rows = sheets
                .SelectMany(sheet =>
                {
                    if (sheet.Id?.Value is null)
                        return Enumerable.Empty<Row>();

                    var worksheetPart =
                        (WorksheetPart)workbookPart.GetPartById(
                            sheet.Id.Value);

                    var worksheet = worksheetPart.Worksheet;

                    if (worksheet is null)
                        return Enumerable.Empty<Row>();

                    return worksheet
                        .Descendants<Row>();
                });

            var lines = rows.Select(row =>
            {
                var cells = row
                    .Elements<Cell>()
                    .Select(cell =>
                    {
                        var value =
                            cell.CellValue?.Text
                            ?? string.Empty;

                        if (cell.DataType?.Value ==
                            CellValues.SharedString)
                        {
                            if (int.TryParse(
                                value,
                                out var index))
                            {
                                return workbookPart
                                    .SharedStringTablePart?
                                    .SharedStringTable?
                                    .Elements<SharedStringItem>()
                                    .ElementAtOrDefault(index)?
                                    .InnerText
                                    ?? value;
                            }
                        }

                        return value;
                    });

                return string.Join(" | ", cells);
            });

            return Task.FromResult(
                string.Join(
                    Environment.NewLine,
                    lines));
        }
    }
}