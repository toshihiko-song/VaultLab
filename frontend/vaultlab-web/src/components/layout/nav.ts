import { FileStack, House, MessageSquareQuote, type LucideIcon } from "lucide-react"

export interface NavItem {
    label: string
    to: string
    icon: LucideIcon
    /** Shown but not navigable until its route exists. Flip to false when you add the page. */
    soon?: boolean
}

export const navItems: NavItem[] = [
    { label: "Home", to: "/", icon: House },
    { label: "Documents", to: "/documents", icon: FileStack, soon: true },
    { label: "Ask", to: "/ask", icon: MessageSquareQuote, soon: true },
]
