import { Activity, FileText, Download } from "lucide-react"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"

const records = [
  { title: "Blood Test Results", date: "13 March 2026", doctor: "Dr. Calvin Carlo", type: "Lab Report", description: "Complete blood count, lipid panel, and metabolic panel results." },
  { title: "Chest X-Ray", date: "5 February 2026", doctor: "Dr. Cristino Murphy", type: "Imaging", description: "Routine chest X-ray showing normal results with no abnormalities detected." },
  { title: "ECG Report", date: "20 January 2026", doctor: "Dr. Calvin Carlo", type: "Diagnostic", description: "Electrocardiogram showing normal sinus rhythm with no signs of arrhythmia." },
  { title: "Covid-19 Test", date: "10 December 2025", doctor: "Dr. Alia Reddy", type: "Lab Report", description: "RT-PCR test result: Negative. No viral RNA detected." },
]

export default function MedicalRecordsPage() {
  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Medical Records</h1>
        <p className="text-sm text-muted-foreground">Access your health history and test results</p>
      </div>

      <div className="flex flex-col gap-4">
        {records.map((record) => (
          <Card key={record.title} className="border-border shadow-sm">
            <CardContent className="flex flex-col gap-4 p-5 sm:flex-row sm:items-start">
              <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-xl bg-primary/10">
                <Activity className="h-6 w-6 text-primary" />
              </div>
              <div className="flex-1">
                <div className="flex items-center gap-3">
                  <h3 className="text-sm font-semibold text-foreground">{record.title}</h3>
                  <span className="rounded-full bg-secondary px-2.5 py-0.5 text-xs font-medium text-muted-foreground">{record.type}</span>
                </div>
                <p className="mt-1 text-xs text-muted-foreground">{record.doctor} | {record.date}</p>
                <p className="mt-2 text-sm leading-relaxed text-muted-foreground">{record.description}</p>
              </div>
              <button className="flex shrink-0 items-center gap-2 rounded-lg border border-border bg-transparent px-4 py-2 text-sm font-medium text-muted-foreground transition-colors hover:bg-secondary hover:text-foreground" aria-label="Download record">
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
