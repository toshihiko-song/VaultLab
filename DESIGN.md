---
name: VaultLab
description: Document intelligence for small teams; every answer points back to the catalogued document it came from.
colors:
  wall: "#18372f"
  wall-foreground: "#eef3ee"
  wall-muted: "#a9bdb2"
  wall-raised: "#224a3f"
  wall-border: "#2c5348"
  signal: "#c8f169"
  signal-foreground: "#142b24"
  focus-olive: "#5e8a1e"
  label-stock: "#ffffff"
  gallery-floor: "#f6f6f3"
  ink: "#141714"
  muted-surface: "#ecede8"
  muted-ink: "#565e58"
  hairline: "#d9dcd6"
  input-stroke: "#c9cdc5"
  status-uploaded: "#4c5a7a"
  status-processing: "#8a5a00"
  status-processed: "#2c6a37"
  status-failed: "#b3261e"
  wall-dark: "#132c25"
  gallery-floor-dark: "#0d1311"
  label-stock-dark: "#161d1a"
  ink-dark: "#e8ede9"
  muted-surface-dark: "#1c2420"
  muted-ink-dark: "#9aa69f"
  hairline-dark: "#26322d"
  input-stroke-dark: "#33413a"
  status-uploaded-dark: "#9fb0d4"
  status-processing-dark: "#e3b55a"
  status-processed-dark: "#8fd49b"
  status-failed-dark: "#f2877f"
typography:
  display:
    fontFamily: "Geist Variable, ui-sans-serif, system-ui, sans-serif"
    fontSize: "clamp(2.25rem, 1.2rem + 4.2vw, 4.75rem)"
    fontWeight: 600
    lineHeight: 0.98
    letterSpacing: "-0.04em"
  headline:
    fontFamily: "Geist Variable, ui-sans-serif, system-ui, sans-serif"
    fontSize: "clamp(2rem, 1.4rem + 2.4vw, 2.75rem)"
    fontWeight: 600
    lineHeight: 1.05
    letterSpacing: "-0.035em"
  title:
    fontFamily: "Geist Variable, ui-sans-serif, system-ui, sans-serif"
    fontSize: "20px"
    fontWeight: 600
    lineHeight: 1.4
    letterSpacing: "-0.02em"
  label-title:
    fontFamily: "Geist Variable, ui-sans-serif, system-ui, sans-serif"
    fontSize: "17px"
    fontWeight: 600
    lineHeight: 1.375
    letterSpacing: "-0.01em"
  section:
    fontFamily: "Geist Variable, ui-sans-serif, system-ui, sans-serif"
    fontSize: "16px"
    fontWeight: 600
    lineHeight: 1.5
  body:
    fontFamily: "Geist Variable, ui-sans-serif, system-ui, sans-serif"
    fontSize: "16px"
    fontWeight: 400
    lineHeight: 1.5
  body-small:
    fontFamily: "Geist Variable, ui-sans-serif, system-ui, sans-serif"
    fontSize: "14px"
    fontWeight: 400
    lineHeight: 1.43
  data:
    fontFamily: "Geist Mono Variable, ui-monospace, monospace"
    fontSize: "12px"
    fontWeight: 400
    lineHeight: 1.33
    letterSpacing: "0.02em"
    fontFeature: "tnum"
  status:
    fontFamily: "Geist Mono Variable, ui-monospace, monospace"
    fontSize: "11px"
    fontWeight: 500
    letterSpacing: "0.02em"
    fontFeature: "tnum"
rounded:
  label: "4px"
  chip: "6px"
  control: "8px"
  ask-bar: "12px"
  dot: "9999px"
spacing:
  xs: "4px"
  sm: "8px"
  md: "16px"
  label-x: "20px"
  lg: "28px"
  xl: "40px"
  2xl: "48px"
  gutter-lg: "56px"
  wall-width: "264px"
components:
  button-primary:
    backgroundColor: "{colors.signal}"
    textColor: "{colors.signal-foreground}"
    rounded: "{rounded.control}"
    padding: "0 16px"
    height: "40px"
  button-ask:
    backgroundColor: "{colors.signal}"
    textColor: "{colors.signal-foreground}"
    rounded: "{rounded.control}"
    padding: "0 20px"
    height: "48px"
  button-outline:
    backgroundColor: "{colors.label-stock}"
    textColor: "{colors.ink}"
    rounded: "{rounded.control}"
    padding: "0 16px"
    height: "40px"
  ask-bar:
    backgroundColor: "{colors.label-stock}"
    textColor: "{colors.ink}"
    rounded: "{rounded.ask-bar}"
    padding: "12px 12px 12px 24px"
  example-chip:
    backgroundColor: "{colors.muted-surface}"
    textColor: "{colors.ink}"
    rounded: "{rounded.chip}"
    padding: "4px 10px"
  accession-label:
    backgroundColor: "{colors.label-stock}"
    textColor: "{colors.ink}"
    rounded: "{rounded.label}"
    padding: "16px 20px"
  status-stamp:
    textColor: "{colors.status-processed}"
    typography: "{typography.status}"
  wall-nav-item:
    textColor: "{colors.wall-muted}"
    rounded: "{rounded.control}"
    padding: "0 12px"
    height: "40px"
  wall-nav-item-active:
    backgroundColor: "{colors.wall-raised}"
    textColor: "{colors.wall-foreground}"
    rounded: "{rounded.control}"
---

# Design System: VaultLab

## Overview

**Creative North Star: "The Gallery Label"**

VaultLab treats every document like a catalogued museum piece. A deep spruce painted wall carries the shell (the sidebar, the login's brand panel, the phone's top band), and the work happens beside it on white label stock: a pale gallery floor, white surfaces, hairline rules and small square-ish label corners. Each document is hung with an accession label (catalogue number, title, medium, state) so an answer can always point back to the exact piece it came from.

The system is quiet and exact. Density is moderate, the chrome stays out of the way of the question, and the one saturated colour, a lime signal, appears only on the path to an answer. Type is a single grotesque (Geist) set tight and heavy for headings, with Geist Mono reserved for data: accession numbers, file measurements and status codes. The deliberate rejection is the zinc-plus-violet AI chat workspace; VaultLab reads as a catalogue, not a chatbot.

Light and dark are the same gallery: in dark mode the floor goes spruce-black, labels become deep green-grey stock, and the wall stays painted.

**Key Characteristics:**
- Spruce wall for the shell, label stock for the work.
- One lime signal with one job: wayfinding to an answer.
- Accession labels as the unit of content; mono only for data.
- Status flips in place and never reflows; Failed is stamped, never hidden.
- Flat surfaces with hairline rules; soft, low shadows only where a label lifts or the ask bar sits.

## Colors

A restrained spruce-and-paper palette with a single lime signal and four muted status inks.

### Primary
- **Gallery Lime** (signal): the path to an answer and nothing else. The Ask button, the primary call to action, the active nav icon on the wall, text selection, focus rings on the wall, and the 4px halo around the focused ask bar. Always paired with Deep Spruce Ink (signal-foreground) for text on it. Identical in light and dark.
- **Focus Olive** (focus-olive): the light-mode focus ring and caret on label stock, where raw lime would not hold contrast against white. In dark mode the ring is Gallery Lime itself.

### Secondary
- **Painted Spruce Wall** (wall): the shell surface. Sidebar, login brand panel, phone top band and mobile menu sheet. Dark mode uses a deeper wall (wall-dark).
- **Wall Lettering** (wall-foreground), **Wall Caption** (wall-muted), **Raised Wall** (wall-raised), **Wall Seam** (wall-border): text, secondary text, active/hover nav fill and dividers on the wall.

### Tertiary
- **Status inks**: Uploaded slate (status-uploaded), Processing amber (status-processing), Processed green (status-processed), Failed red (status-failed). Used only as the text and dot colour of status stamps; dark mode lifts each to a lighter tint (the `-dark` keys).

### Neutral
- **Gallery Floor** (gallery-floor): the page ground beside the wall.
- **Label Stock** (label-stock): cards, accession labels, the ask bar, outline buttons, popovers.
- **Gallery Ink** (ink): primary text, a green-tinted near-black.
- **Muted Surface** (muted-surface) and **Caption Ink** (muted-ink): example chips, hover fills, secondary text, accession numbers and measurements.
- **Hairline** (hairline): every rule and card border. **Input Stroke** (input-stroke): field strokes, dashed ghost labels and label hover borders.

### Named Rules
**The One Job Rule.** Lime marks the path to an answer: Ask, the primary action, the active wayfinding cue, focus. It never appears on the VaultLab mark, on illustration, on headings, or as decoration. If a lime element does not move the user toward a question or its answer, it is wrong.

**The Painted Wall Rule.** Spruce is for the shell that holds navigation and brand. Work surfaces sit on label stock beside it; content is never laid directly on the wall except brand lettering and the hung VaultLab label on login.

**The Stamped Status Rule.** Status colour lives in the status stamp's text and dot only, never as a filled pill or a row tint. Failed uses the same stamp as every other state.

## Typography

**Display Font:** Geist Variable (with ui-sans-serif, system-ui)
**Body Font:** Geist Variable (one family for display, headings and body)
**Label/Mono Font:** Geist Mono Variable (with ui-monospace), tabular figures

**Character:** One confident grotesque, set heavy (600) and tightly tracked at size, reads like painted wall lettering; the mono sits underneath it like the typed lines on a museum label.

### Hierarchy
- **Display** (600, clamp 2.25rem to 4.75rem, 0.98, -0.04em): wall lettering on the login brand panel only, painted on line by line.
- **Headline** (600, clamp 2rem to 2.75rem, 1.05, -0.035em): the one page heading on a work surface ("Ask the collection."). The login form heading is a smaller step of the same voice (28px, 32px from sm, -0.03em).
- **Title** (600, 20px, -0.02em): empty-state and sub-section headings.
- **Label Title** (600, 17px, snug, -0.01em): the document title on an accession label, clamped to two lines.
- **Section** (600, 16px): section heads over a hairline ("Recently accessioned").
- **Body** (400, 16px; 17px for the lede from sm): explanatory copy, held to 52 to 60ch.
- **Body Small** (400, 14px): example chips, nav items, footnotes.
- **Data** (Geist Mono, 12px, 0.02em, tabular): accession numbers (VL-0001), medium and size (PDF · 2.4 MB), counts, dates.
- **Status** (Geist Mono 500, 11px, uppercase, tabular): status codes in stamps and availability codes on the wall nav.

### Named Rules
**The Mono Is Data Rule.** Geist Mono with tabular figures is for catalogue data only: accession numbers, file measurements, counts, dates, status codes. Sentences, headings and buttons are never mono.

**The Uppercase Is A Code Rule.** Uppercase appears only on mono status codes. Headings and labels stay in sentence case.

## Layout

The app is a two-column gallery at desktop (from 1024px): a fixed 264px spruce wall on the left, sticky at full viewport height, and the work area beside it. The wall is painted with a background gradient as well as the sidebar, so it runs the full page height. Below 1024px the wall becomes a 56px sticky top band holding the wordmark and a menu button; the menu opens as a left sheet painted as the wall (min(18rem, 85vw)).

The work area is a single column capped at 1152px (max-w-6xl), with side gutters of 16px, 32px from 640px, and 56px from 1024px; top padding steps 32/48/56px. Sections are separated by 40px (48px at desktop); within a section, heading groups use 12px and blocks use 20 to 28px. Accession labels hang in a grid: one column, two from 640px, three from 1280px, with 16px gaps. Empty grid positions show as dashed hanging slots.

Login splits the viewport roughly 1.1 : 1 at desktop, wall left and sign-in on the floor right; on phones the wall is a top band above the form. The sign-in column is capped at 384px.

## Elevation & Depth

Mostly flat. Depth comes from the wall/floor/stock contrast and hairline rules, with a small vocabulary of soft, low, ink-tinted shadows: a 1px hairline "shelf" under resting labels and buttons, a gentle lift when a label is hovered, a long faint drop under the ask bar, and one deep cast shadow for the label hung on the login wall. There are no hard offset shadows.

### Shadow Vocabulary
- **Shelf line** (`box-shadow: 0 1px 0 var(--border)`): resting accession labels and the Google sign-in button; a printed label sitting on a ledge.
- **Label lift** (`box-shadow: 0 6px 16px -10px rgb(20 23 20 / 0.35)`): accession label on hover, with the border stepping to Input Stroke.
- **Ask bar rest** (`box-shadow: 0 1px 0 var(--border), 0 12px 32px -24px rgb(20 23 20 / 0.4)`): the ask bar, the one raised object on the floor.
- **Ask halo** (`box-shadow: 0 0 0 4px color-mix(in oklab, var(--signal) 45%, transparent)`): the ask bar's focus-within state, with the border turning to the ring colour.
- **Wall cast** (`box-shadow: 0 18px 40px -20px rgb(0 0 0 / 0.6)`): the VaultLab label hung on the login wall.

### Named Rules
**The Flat Floor Rule.** Surfaces rest flat on hairlines. A shadow means an object is hung or raised (a label, the ask bar); it is never decoration on a section or container.

## Shapes

Small, near-square corners in three steps: 4px for labels (accession labels, ghost slots, the hung login label), 8px for controls (buttons, nav items, the lifecycle strip), 12px for the ask bar, the single largest object. Example chips sit at 6px. Borders are 1px hairlines; ghost labels and empty hanging slots use a dashed 1px stroke. Status dots are 6px circles. The mark is a label outline (rounded rectangle, heavier title line, lighter caption lines) drawn in currentColor.

## Components

### Buttons
Plain and functional; the lime one is the way forward.
- **Shape:** gently squared (8px).
- **Primary:** Gallery Lime with Deep Spruce Ink text, 600 weight, 40px tall with 16px sides on page actions; the Ask button is 44px (48px from sm) with 20px sides, an inner bottom shade (`inset 0 -1px 0 rgb(0 0 0 / 0.12)`) and a trailing arrow icon. One primary per view region.
- **Hover / Focus:** Ask darkens slightly (brightness 95%); the generic primary variant currently fades to 80% lime instead; focus shows a 3px ring at half the ring colour with the border set to the ring; pressed buttons drop 1px.
- **Outline:** Label Stock fill, hairline border, ink text; used for secondary page actions (Upload document) and Google sign-in (48px, 15px medium, shelf line, border shifts toward the ring on hover).
- **Ghost on the wall:** Wall Caption text, Raised Wall fill at 60% on hover, lime focus ring (Sign out, menu button).

### Chips
- **Style:** example questions after a quiet "Try" lead-in: Muted Surface fill, transparent border, 14px ink at 80%, 6px corners, 4px by 10px padding.
- **State:** hover reveals an Input Stroke border and full ink; focus shows a 2px ring. Clicking fills the ask bar; chips never submit.

### Cards / Containers
- **Corner Style:** 4px labels; 8px for the lifecycle strip.
- **Background:** Label Stock on Gallery Floor.
- **Shadow Strategy:** shelf line at rest, label lift on hover (see Elevation & Depth).
- **Border:** 1px Hairline; internal rules are hairlines too (the label's measurement row, the lifecycle strip's dividers).
- **Internal Padding:** 16px by 20px.

### Inputs / Fields
- **Style:** the ask bar is the signature field: a 12px-rounded Label Stock bar spanning the content width, borderless 20px input text (16px on phones), Caption Ink placeholder, lime Ask button inside on the right. Generic inputs are 8px-rounded with an Input Stroke border.
- **Focus:** the ask bar's border turns to the ring colour and it gains the 4px lime halo; generic inputs show a 3px ring at half the ring colour. The caret is the ring colour.
- **Error / Disabled:** destructive border with a faint destructive ring; disabled at 50% opacity.

### Navigation
- **Style:** the wall nav: 40px rows, 8px corners, 14px Geist, icon plus label, Wall Caption text.
- **States:** hover fills Raised Wall at 60% with Wall Lettering text; active fills Raised Wall, 500 weight, Wall Lettering text, and the icon turns Gallery Lime (the wayfinding cue). Unavailable routes stay visible but inert, with a mono uppercase "SOON" code at the right.
- **Foot:** a three-way light/dark/system segmented switch on a darkened well (black 15%), active segment Raised Wall with a small shadow; then Sign out. Separated from the nav by a Wall Seam rule.
- **Mobile:** the wall collapses to the top band; the same nav and foot open in a left sheet.

### Accession Label (signature)
The unit of content. Top row: mono accession number (VL-0142) left, status stamp right. Middle: the document title in Label Title, clamped to two lines. Bottom row, under a hairline: mono medium and size left, mono date right. A **ghost** variant (dashed Input Stroke border, transparent fill, muted title) shows the format before any document exists; plain dashed slots fill the rest of the empty grid row.

### Status Stamp (signature)
Mono 11px uppercase status code with a 6px dot in the same status ink, right-aligned in a fixed 10.5ch minimum width so a label never reflows as a document moves Uploaded, Processing, Processed. Processing's dot pulses (disabled under reduced motion). Failed uses the identical stamp in Failed red and stays in the list.

### Motion
One authored moment, on login only: wall lettering is painted on line by line (clip-path reveal, 900ms, expo ease-out `cubic-bezier(0.16, 1, 0.3, 1)`, 120ms stagger), then the VaultLab label hangs into place (700ms from a 6px rise, slight tilt and blur, 360ms delay). Elsewhere, state changes are 200ms colour, border and shadow transitions. All entrance motion is removed under reduced motion.

## Do's and Don'ts

### Do:
- **Do** keep the shell on the Painted Spruce Wall and the work on Label Stock over the Gallery Floor.
- **Do** reserve Gallery Lime for Ask, the primary action, the active nav cue, selection and focus.
- **Do** use Focus Olive for focus rings and caret on light label stock, and Gallery Lime for focus on the wall and in dark mode.
- **Do** present every document as an accession label: accession number, title, medium, status.
- **Do** set accession numbers, measurements, counts, dates and status codes in Geist Mono with tabular figures.
- **Do** give status stamps a fixed minimum width so status changes never reflow the label.
- **Do** keep a Failed document visible with its stamp.
- **Do** keep corners small: 4px labels, 8px controls, 12px only for the ask bar.

### Don't:
- **Don't** put lime on the VaultLab mark, headings, illustration or any decorative surface.
- **Don't** set sentences, headings or buttons in mono.
- **Don't** fill status as coloured pills or row tints; status is ink and a dot.
- **Don't** hide or collapse failed documents.
- **Don't** drift toward the zinc-plus-violet AI chat workspace look.
- **Don't** add hard offset shadows or shadows on sections and containers; shadows mean a label is hung or the ask bar is raised.
