"use client"

import React from "react"

import { useState } from "react"
import { Menu } from "lucide-react"
import Navbar from "@/components/navbar"
import DashboardSidebar from "@/components/dashboard-sidebar"

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode
}) {
  const [sidebarOpen, setSidebarOpen] = useState(false)

  return (
    <div className="flex min-h-screen flex-col">
      <Navbar />
      <div className="flex flex-1">
        <DashboardSidebar open={sidebarOpen} onClose={() => setSidebarOpen(false)} />
        <main className="flex-1 p-4 lg:p-6">
          <button
            className="mb-4 rounded-lg border border-border bg-card p-2 lg:hidden"
            onClick={() => setSidebarOpen(true)}
            aria-label="Open sidebar"
          >
            <Menu className="h-5 w-5 text-muted-foreground" />
          </button>
          {children}
        </main>
      </div>
    </div>
  )
}
