import { useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { GoogleIcon } from "@/components/brand/GoogleIcon";
import { VaultWordmark } from "@/components/brand/VaultMark";
import { Button } from "@/components/ui/button";
import { useAuth } from "../hooks/useAuth";

export default function LoginPage() {
    const {isAuthenticated, login} = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    const from = location.state?.from?.pathname || "/";

    if(isAuthenticated) {
        return <NavigateToHome />
    } 

    return (
        <div className="grid min-h-dvh grid-rows-[auto_1fr] lg:grid-cols-[minmax(0,1.1fr)_minmax(0,1fr)] lg:grid-rows-1">
            <section
                aria-label="About VaultLab"
                className="relative flex flex-col gap-10 overflow-hidden bg-wall px-5 pt-6 pb-10 text-wall-foreground sm:px-10 sm:pt-8 lg:min-h-dvh lg:justify-between lg:px-14 lg:py-12"
            >
                <VaultWordmark />

                <div className="wall-letter grid gap-1 text-[clamp(2.25rem,1.2rem+4.2vw,4.75rem)] leading-[0.98] font-semibold tracking-[-0.04em]">
                    <span>Answers from</span>
                    <span>your team’s documents.</span>
                    <span className="mt-[0.18em] border-t border-wall-border pt-[0.18em] text-wall-foreground">Nothing else.</span>
                </div>

                <article
                    aria-label="VaultLab, catalogue label"
                    className="label-hang hidden max-w-md gap-3 rounded-sm bg-[#fbfbf8] px-5 py-4 text-[#141714] shadow-[0_18px_40px_-20px_rgb(0_0_0/0.6)] sm:grid"
                >
                    <div className="label-data flex justify-between text-xs text-[#565e58]">
                        <span>VL-0001</span>
                        <span>Est. 2026</span>
                    </div>
                    <p className="text-lg font-semibold tracking-[-0.01em]">VaultLab, for small teams</p>
                    <p className="text-sm leading-relaxed text-pretty text-[#3d443f]">
                        Upload policies, contracts and specs. Ask in plain language. Every answer shows the
                        passages it came from, and when the documents don't say, VaultLab says so.
                    </p>
                    <div className="label-data border-t border-[#e2e4df] pt-3 text-xs text-[#565e58]">
                        PDF · DOCX · XLSX · MD · TXT
                    </div>
                </article>
            </section>

            <section className="flex justify-center px-5 py-10 sm:items-center sm:px-10 lg:py-16">
                <div className="grid h-fit w-full max-w-sm gap-8">
                    <div className="grid gap-3">
                        <h1 className="text-[28px] leading-tight font-semibold tracking-[-0.03em] sm:text-[32px]">
                            Sign in to VaultLab
                        </h1>
                        <p className="text-pretty text-muted-foreground">
                            Use your Google account to open your team’s vault.
                        </p>
                    </div>

                    <Button
                        variant="outline"
                        size="lg"
                        onClick={() => {
                            login();
                            navigate(from, {replace: true})
                        }}
                        className="h-12 gap-3 rounded-lg bg-card text-[15px] font-medium shadow-[0_1px_0_var(--border)] hover:border-ring/60 hover:bg-card"
                    >
                        <GoogleIcon className="size-5" />
                        Continue with Google
                    </Button>

                    <p className="border-t pt-6 text-sm leading-relaxed text-pretty text-muted-foreground">
                        Answers come only from the documents your team uploads, and each one shows its sources.
                    </p>
                </div>
            </section>
        </div>
    )
}

function NavigateToHome() {
    const navigate = useNavigate();

    useEffect(() => {
        navigate("/", {replace: true})
    }, [navigate])

    return null;
}
