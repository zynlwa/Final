import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"

const patients = [
  {
    name: "Christopher Burrell",
    age: 25,
    gender: "Female",
    bloodGroup: "B+",
    phone: "+(125) 458-8547",
    lastVisit: "13 Mar 2026",
    status: "Active",
    avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=80&h=80&fit=crop&crop=face",
  },
  {
    name: "Sarah Johnson",
    age: 32,
    gender: "Female",
    bloodGroup: "O+",
    phone: "+(125) 321-4567",
    lastVisit: "14 Mar 2026",
    status: "Active",
    avatar: "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=80&h=80&fit=crop&crop=face",
  },
  {
    name: "Michael Smith",
    age: 45,
    gender: "Male",
    bloodGroup: "A-",
    phone: "+(125) 654-3210",
    lastVisit: "15 Mar 2026",
    status: "Inactive",
    avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=80&h=80&fit=crop&crop=face",
  },
  {
    name: "Emily Davis",
    age: 28,
    gender: "Female",
    bloodGroup: "AB+",
    phone: "+(125) 987-6543",
    lastVisit: "16 Mar 2026",
    status: "Active",
    avatar: "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=80&h=80&fit=crop&crop=face",
  },
  {
    name: "David Wilson",
    age: 38,
    gender: "Male",
    bloodGroup: "O-",
    phone: "+(125) 111-2233",
    lastVisit: "17 Mar 2026",
    status: "Active",
    avatar: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=80&h=80&fit=crop&crop=face",
  },
]

export default function PatientsPage() {
  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Patients</h1>
        <p className="text-sm text-muted-foreground">Manage your patient records</p>
      </div>

      <Card className="border-border shadow-sm">
        <CardHeader className="pb-3">
          <CardTitle className="text-lg font-semibold text-foreground">
            All Patients
          </CardTitle>
        </CardHeader>
        <CardContent>
          <div className="hidden grid-cols-7 gap-4 rounded-lg bg-secondary px-4 py-3 text-xs font-semibold uppercase text-muted-foreground md:grid">
            <span>Patient</span>
            <span>Age</span>
            <span>Gender</span>
            <span>Blood Group</span>
            <span>Phone</span>
            <span>Last Visit</span>
            <span>Status</span>
          </div>
          <div className="flex flex-col">
            {patients.map((p) => (
              <div
                key={p.name}
                className="grid grid-cols-1 gap-2 border-b border-border p-4 last:border-0 md:grid-cols-7 md:items-center md:gap-4"
              >
                <div className="flex items-center gap-3">
                  <Avatar className="h-9 w-9">
                    <AvatarImage src={p.avatar || "/placeholder.svg"} alt={p.name} />
                    <AvatarFallback>{p.name[0]}</AvatarFallback>
                  </Avatar>
                  <span className="text-sm font-medium text-foreground">{p.name}</span>
                </div>
                <p className="text-sm text-muted-foreground">{p.age}</p>
                <p className="text-sm text-muted-foreground">{p.gender}</p>
                <p className="text-sm text-muted-foreground">{p.bloodGroup}</p>
                <p className="text-sm text-muted-foreground">{p.phone}</p>
                <p className="text-sm text-muted-foreground">{p.lastVisit}</p>
                <div>
                  <Badge
                    variant="secondary"
                    className={`border-0 ${p.status === "Active" ? "bg-emerald-100 text-emerald-700" : "bg-secondary text-muted-foreground"}`}
                  >
                    {p.status}
                  </Badge>
                </div>
              </div>
            ))}
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
