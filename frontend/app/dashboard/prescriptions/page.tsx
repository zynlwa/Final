import { FileText, Download } from "lucide-react"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"

const prescriptions = [
  { id: "RX-001", medication: "Amoxicillin 500mg", doctor: "Dr. Calvin Carlo", date: "13 March 2026", dosage: "3 times daily", duration: "7 days", status: "Active" },
  { id: "RX-002", medication: "Ibuprofen 400mg", doctor: "Dr. Cristino Murphy", date: "5 May 2026", dosage: "2 times daily", duration: "5 days", status: "Active" },
  { id: "RX-003", medication: "Vitamin D 1000IU", doctor: "Dr. Alia Reddy", date: "19 June 2026", dosage: "Once daily", duration: "30 days", status: "Active" },
  { id: "RX-004", medication: "Paracetamol 500mg", doctor: "Dr. Calvin Carlo", date: "1 Feb 2026", dosage: "As needed", duration: "3 days", status: "Completed" },
]

export default function PrescriptionsPage() {
  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Prescriptions</h1>
        <p className="text-sm text-muted-foreground">Your active and past prescriptions</p>
      </div>

      <div className="flex flex-col gap-4">
        {prescriptions.map((rx) => (
          <Card key={rx.id} className="border-border shadow-sm">
            <CardContent className="flex flex-col gap-4 p-5 sm:flex-row sm:items-center">
              <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-xl bg-primary/10">
                <FileText className="h-6 w-6 text-primary" />
              </div>
              <div className="flex-1">
                <div className="flex items-center gap-2">
                  <h3 className="text-sm font-semibold text-foreground">{rx.medication}</h3>
                  <Badge variant="secondary" className={`border-0 text-xs ${rx.status === "Active" ? "bg-emerald-100 text-emerald-700" : "bg-secondary text-muted-foreground"}`}>{rx.status}</Badge>
                </div>
                <p className="mt-1 text-xs text-muted-foreground">{rx.doctor} | {rx.date}</p>
                <p className="mt-1 text-xs text-muted-foreground">Dosage: {rx.dosage} | Duration: {rx.duration}</p>
              </div>
              <button className="flex items-center gap-2 rounded-lg border border-border bg-transparent px-4 py-2 text-sm font-medium text-muted-foreground transition-colors hover:bg-secondary hover:text-foreground" aria-label="Download prescription">
                <Download className="h-4 w-4" />
                Download
              </button>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  )
}
