"use client"

import { useState } from "react"
import { Send, Search } from "lucide-react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Card, CardContent } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { cn } from "@/lib/utils"

const contacts = [
  {
    id: 1,
    name: "Christopher Burrell",
    lastMessage: "Thank you doctor!",
    time: "2m ago",
    unread: 2,
    avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=80&h=80&fit=crop&crop=face",
    online: true,
  },
  {
    id: 2,
    name: "Sarah Johnson",
    lastMessage: "When is my next appointment?",
    time: "1h ago",
    unread: 0,
    avatar: "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=80&h=80&fit=crop&crop=face",
    online: true,
  },
  {
    id: 3,
    name: "Michael Smith",
    lastMessage: "I will send the reports",
    time: "3h ago",
    unread: 1,
    avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=80&h=80&fit=crop&crop=face",
    online: false,
  },
  {
    id: 4,
    name: "Emily Davis",
    lastMessage: "Can I reschedule?",
    time: "1d ago",
    unread: 0,
    avatar: "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=80&h=80&fit=crop&crop=face",
    online: false,
  },
]

const messages = [
  { id: 1, sender: "patient", text: "Hello Doctor, I wanted to ask about my test results.", time: "10:00 AM" },
  { id: 2, sender: "doctor", text: "Hello Christopher! Your results look great. Everything is within the normal range.", time: "10:02 AM" },
  { id: 3, sender: "patient", text: "That's a relief! Do I need to come for a follow-up?", time: "10:05 AM" },
  { id: 4, sender: "doctor", text: "Yes, I'd recommend a follow-up in 2 weeks. You can book through the appointment page.", time: "10:07 AM" },
  { id: 5, sender: "patient", text: "Thank you doctor!", time: "10:08 AM" },
]

export default function MessagesPage() {
  const [selectedContact, setSelectedContact] = useState(contacts[0])
  const [newMessage, setNewMessage] = useState("")

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Messages</h1>
        <p className="text-sm text-muted-foreground">Chat with your patients</p>
      </div>

      <Card className="flex h-[600px] overflow-hidden border-border shadow-sm">
        {/* Contact list */}
        <div className="flex w-72 shrink-0 flex-col border-r border-border">
          <div className="border-b border-border p-3">
            <div className="relative">
              <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
              <Input placeholder="Search..." className="border-border bg-card pl-9 text-sm" />
            </div>
          </div>
          <div className="flex-1 overflow-y-auto">
            {contacts.map((contact) => (
              <button
                key={contact.id}
                onClick={() => setSelectedContact(contact)}
                className={cn(
                  "flex w-full items-center gap-3 border-b border-border px-3 py-3 text-left transition-colors hover:bg-secondary/50",
                  selectedContact.id === contact.id && "bg-primary/5"
                )}
              >
                <div className="relative">
                  <Avatar className="h-10 w-10">
                    <AvatarImage src={contact.avatar || "/placeholder.svg"} alt={contact.name} />
                    <AvatarFallback>{contact.name[0]}</AvatarFallback>
                  </Avatar>
                  {contact.online && (
                    <span className="absolute bottom-0 right-0 h-3 w-3 rounded-full border-2 border-card bg-emerald-400" />
                  )}
                </div>
                <div className="flex-1 overflow-hidden">
                  <div className="flex items-center justify-between">
                    <p className="truncate text-sm font-medium text-foreground">{contact.name}</p>
                    <span className="ml-2 shrink-0 text-xs text-muted-foreground">{contact.time}</span>
                  </div>
                  <p className="truncate text-xs text-muted-foreground">{contact.lastMessage}</p>
                </div>
                {contact.unread > 0 && (
                  <span className="flex h-5 w-5 shrink-0 items-center justify-center rounded-full bg-primary text-xs font-medium text-primary-foreground">
                    {contact.unread}
                  </span>
                )}
              </button>
            ))}
          </div>
        </div>

        {/* Chat area */}
        <div className="flex flex-1 flex-col">
          <div className="flex items-center gap-3 border-b border-border px-4 py-3">
            <Avatar className="h-9 w-9">
              <AvatarImage src={selectedContact.avatar || "/placeholder.svg"} alt={selectedContact.name} />
              <AvatarFallback>{selectedContact.name[0]}</AvatarFallback>
            </Avatar>
            <div>
              <p className="text-sm font-medium text-foreground">{selectedContact.name}</p>
              <p className="text-xs text-emerald-500">
                {selectedContact.online ? "Online" : "Offline"}
              </p>
            </div>
          </div>

          <CardContent className="flex flex-1 flex-col gap-3 overflow-y-auto p-4">
            {messages.map((msg) => (
              <div
                key={msg.id}
                className={cn("flex", msg.sender === "doctor" ? "justify-end" : "justify-start")}
              >
                <div
                  className={cn(
                    "max-w-xs rounded-xl px-4 py-2.5",
                    msg.sender === "doctor"
                      ? "bg-primary text-primary-foreground"
                      : "bg-secondary text-foreground"
                  )}
                >
                  <p className="text-sm">{msg.text}</p>
                  <p
                    className={cn(
                      "mt-1 text-xs",
                      msg.sender === "doctor" ? "text-primary-foreground/70" : "text-muted-foreground"
                    )}
                  >
                    {msg.time}
                  </p>
                </div>
              </div>
            ))}
          </CardContent>

          <div className="border-t border-border p-3">
            <div className="flex items-center gap-2">
              <Input
                placeholder="Type a message..."
                value={newMessage}
                onChange={(e) => setNewMessage(e.target.value)}
                className="flex-1 border-border bg-card"
              />
              <button className="flex h-10 w-10 items-center justify-center rounded-lg bg-primary text-primary-foreground transition-opacity hover:opacity-90" aria-label="Send message">
                <Send className="h-4 w-4" />
              </button>
            </div>
          </div>
        </div>
      </Card>
    </div>
  )
}
