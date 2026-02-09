import { FileText, Download, DollarSign } from "lucide-react"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { cn } from "@/lib/utils"

const invoices = [
  { id: "INV-001", patient: "Christopher Burrell", amount: "$150", date: "13 Mar 2026", status: "Paid" },
  { id: "INV-002", patient: "Sarah Johnson", amount: "$200", date: "14 Mar 2026", status: "Paid" },
  { id: "INV-003", patient: "Michael Smith", amount: "$120", date: "15 Mar 2026", status: "Pending" },
  { id: "INV-004", patient: "Emily Davis", amount: "$250", date: "16 Mar 2026", status: "Overdue" },
  { id: "INV-005", patient: "David Wilson", amount: "$180", date: "17 Mar 2026", status: "Paid" },
]

function getStatusClasses(status: string) {
  switch (status) {
    case "Paid":
      return "bg-emerald-100 text-emerald-700"
    case "Pending":
      return "bg-amber-100 text-amber-700"
    case "Overdue":
      return "bg-red-100 text-red-700"
    default:
      return "bg-secondary text-secondary-foreground"
  }
}

export default function InvoicesPage() {
  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Invoices</h1>
        <p className="text-sm text-muted-foreground">Manage billing and invoices</p>
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-3">
        <Card className="border-border shadow-sm">
          <CardContent className="flex items-center gap-4 p-5">
            <div className="flex h-12 w-12 items-center justify-center rounded-xl bg-emerald-100">
              <DollarSign className="h-6 w-6 text-emerald-600" />
            </div>
            <div>
              <p className="text-sm text-muted-foreground">Total Paid</p>
              <p className="text-xl font-bold text-foreground">$530</p>
            </div>
          </CardContent>
        </Card>
        <Card className="border-border shadow-sm">
          <CardContent className="flex items-center gap-4 p-5">
            <div className="flex h-12 w-12 items-center justify-center rounded-xl bg-amber-100">
              <DollarSign className="h-6 w-6 text-amber-600" />
            </div>
            <div>
              <p className="text-sm text-muted-foreground">Pending</p>
              <p className="text-xl font-bold text-foreground">$120</p>
            </div>
          </CardContent>
        </Card>
        <Card className="border-border shadow-sm">
          <CardContent className="flex items-center gap-4 p-5">
            <div className="flex h-12 w-12 items-center justify-center rounded-xl bg-red-100">
              <DollarSign className="h-6 w-6 text-red-600" />
            </div>
            <div>
              <p className="text-sm text-muted-foreground">Overdue</p>
              <p className="text-xl font-bold text-foreground">$250</p>
            </div>
          </CardContent>
        </Card>
      </div>

      <Card className="border-border shadow-sm">
        <CardHeader className="pb-3">
          <CardTitle className="text-lg font-semibold text-foreground">
            All Invoices
          </CardTitle>
        </CardHeader>
        <CardContent>
          <div className="hidden grid-cols-6 gap-4 rounded-lg bg-secondary px-4 py-3 text-xs font-semibold uppercase text-muted-foreground md:grid">
            <span>Invoice ID</span>
            <span>Patient</span>
            <span>Amount</span>
            <span>Date</span>
            <span>Status</span>
            <span>Action</span>
          </div>
          <div className="flex flex-col">
            {invoices.map((inv) => (
              <div
                key={inv.id}
                className="grid grid-cols-1 gap-2 border-b border-border p-4 last:border-0 md:grid-cols-6 md:items-center md:gap-4"
              >
                <div className="flex items-center gap-2">
                  <FileText className="h-4 w-4 text-primary" />
                  <span className="text-sm font-medium text-foreground">{inv.id}</span>
                </div>
                <p className="text-sm text-muted-foreground">{inv.patient}</p>
                <p className="text-sm font-semibold text-foreground">{inv.amount}</p>
                <p className="text-sm text-muted-foreground">{inv.date}</p>
                <div>
                  <Badge variant="secondary" className={cn("border-0", getStatusClasses(inv.status))}>
                    {inv.status}
                  </Badge>
                </div>
                <button className="flex items-center gap-1 text-sm font-medium text-primary hover:underline" aria-label={`Download ${inv.id}`}>
                  <Download className="h-4 w-4" />
                  Download
                </button>
              </div>
            ))}
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
