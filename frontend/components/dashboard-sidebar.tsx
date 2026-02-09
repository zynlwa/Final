"use client"

import Link from "next/link"
import { usePathname } from "next/navigation"
import {
  LayoutDashboard,
  CalendarDays,
  Clock,
  FileText,
  MessageSquare,
  User,
  Settings,
  Users,
  Star,
  X,
  Heart,
  Stethoscope,
  CreditCard,
  Droplets,
} from "lucide-react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { cn } from "@/lib/utils"
import { useAuth } from "@/context/auth-context"

const doctorLinks = [
  { name: "Dashboard", href: "/dashboard", icon: LayoutDashboard },
  { name: "Appointment", href: "/dashboard/appointments", icon: CalendarDays },
  { name: "Schedule Timing", href: "/dashboard/schedule", icon: Clock },
  { name: "Invoices", href: "/dashboard/invoices", icon: FileText },
  { name: "Messages", href: "/dashboard/messages", icon: MessageSquare },
  { name: "Profile", href: "/dashboard/profile", icon: User },
  { name: "Profile Settings", href: "/dashboard/profile-settings", icon: Settings },
  { name: "Patients", href: "/dashboard/patients", icon: Users },
  { name: "Patients Review", href: "/dashboard/reviews", icon: Star },
]

const patientLinks = [
  { name: "Dashboard", href: "/dashboard", icon: LayoutDashboard },
  { name: "My Appointments", href: "/dashboard/my-appointments", icon: CalendarDays },
  { name: "Book Appointment", href: "/book-appointment", icon: Stethoscope },
  { name: "My Doctors", href: "/dashboard/my-doctors", icon: Heart },
  { name: "Prescriptions", href: "/dashboard/prescriptions", icon: FileText },
  { name: "Medical Records", href: "/dashboard/medical-records", icon: Droplets },
  { name: "Invoices", href: "/dashboard/invoices", icon: CreditCard },
  { name: "Messages", href: "/dashboard/messages", icon: MessageSquare },
  { name: "Profile", href: "/dashboard/profile", icon: User },
  { name: "Profile Settings", href: "/dashboard/profile-settings", icon: Settings },
]

export default function DashboardSidebar({
  open,
  onClose,
}: {
  open: boolean
  onClose: () => void
}) {
  const pathname = usePathname()
  const { user, role } = useAuth()

  const sidebarLinks = role === "patient" ? patientLinks : doctorLinks

  const displayName = user?.name ?? "Dr. Calvin Carlo"
  const displayAvatar = user?.avatar ?? "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=100&h=100&fit=crop&crop=face"
  const displaySubtitle = role === "patient" ? (user?.age ?? "Patient") : (user?.specialty ?? "Orthopedic")

  return (
    <>
      {open && (
        <div
          className="fixed inset-0 z-40 bg-foreground/20 backdrop-blur-sm lg:hidden"
          onClick={onClose}
          onKeyDown={(e) => e.key === "Escape" && onClose()}
          role="button"
          tabIndex={0}
          aria-label="Close sidebar"
        />
      )}

      <aside
        className={cn(
          "fixed left-0 top-0 z-50 flex h-full w-72 flex-col border-r border-border bg-card transition-transform duration-300 lg:sticky lg:top-16 lg:z-0 lg:h-[calc(100vh-4rem)] lg:translate-x-0",
          open ? "translate-x-0" : "-translate-x-full"
        )}
      >
        <div className="flex items-center justify-between border-b border-border p-4 lg:hidden">
          <span className="text-sm font-semibold text-foreground">Menu</span>
          <button onClick={onClose} aria-label="Close menu">
            <X className="h-5 w-5 text-muted-foreground" />
          </button>
        </div>

        <div className="flex flex-col items-center border-b border-border pb-6 pt-4">
          <div className="relative mb-3 w-full">
            {role === "patient" ? (
              <div className="relative h-24 w-full overflow-hidden rounded-t-lg">
                <svg viewBox="0 0 400 120" className="h-full w-full" preserveAspectRatio="none">
                  <path d="M0 120 C50 80, 100 100, 150 60 C200 20, 250 80, 300 40 C350 0, 400 50, 400 0 L400 120 Z" fill="hsl(270, 60%, 50%)" />
                  <path d="M0 120 C80 70, 120 90, 200 50 C280 10, 350 70, 400 30 L400 120 Z" fill="hsl(150, 60%, 50%)" opacity="0.7" />
                  <path d="M0 120 C60 90, 150 50, 250 80 C350 110, 380 60, 400 80 L400 120 Z" fill="hsl(45, 80%, 60%)" opacity="0.6" />
                </svg>
              </div>
            ) : (
              <div className="h-24 w-full overflow-hidden rounded-t-lg bg-gradient-to-r from-[hsl(234,85%,30%)] via-[hsl(234,85%,45%)] to-[hsl(234,85%,60%)]">
                <svg viewBox="0 0 300 80" className="h-full w-full opacity-40">
                  <polyline
                    fill="none"
                    stroke="hsl(180, 100%, 70%)"
                    strokeWidth="2"
                    points="0,50 30,50 40,20 50,60 60,35 70,50 100,50 130,50 140,15 150,65 160,30 170,50 200,50 230,50 240,20 250,60 260,35 270,50 300,50"
                  />
                </svg>
              </div>
            )}
            <div className="absolute -bottom-8 left-1/2 -translate-x-1/2">
              <Avatar className="h-16 w-16 border-4 border-card">
                <AvatarImage src={displayAvatar || "/placeholder.svg"} alt={displayName} />
                <AvatarFallback>{displayName.charAt(0)}</AvatarFallback>
              </Avatar>
            </div>
          </div>
          <div className="mt-6 text-center">
            <h3 className="text-base font-semibold text-foreground">{displayName}</h3>
            <p className="text-sm text-primary">{displaySubtitle}</p>
          </div>
        </div>

        <nav className="flex-1 overflow-y-auto p-3">
          <ul className="flex flex-col gap-1">
            {sidebarLinks.map((link) => {
              const isActive = pathname === link.href
              return (
                <li key={link.name}>
                  <Link
                    href={link.href}
                    onClick={onClose}
                    className={cn(
                      "flex items-center gap-3 rounded-lg px-3 py-2.5 text-sm font-medium transition-colors",
                      isActive
                        ? "bg-primary/10 text-primary"
                        : "text-muted-foreground hover:bg-secondary hover:text-foreground"
                    )}
                  >
                    <link.icon className="h-5 w-5" />
                    {link.name}
                  </Link>
                </li>
              )
            })}
          </ul>
        </nav>
      </aside>
    </>
  )
}
