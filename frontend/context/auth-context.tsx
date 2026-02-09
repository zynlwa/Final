"use client"

import React, { createContext, useContext, useState, useEffect, useCallback } from "react"

export type UserRole = "doctor" | "patient" | null

interface UserData {
  name: string
  email: string
  role: UserRole
  avatar: string
  specialty?: string
  age?: string
}

interface AuthContextType {
  user: UserData | null
  role: UserRole
  isLoggedIn: boolean
  login: (role: UserRole, data?: Partial<UserData>) => void
  logout: () => void
}

const AuthContext = createContext<AuthContextType>({
  user: null,
  role: null,
  isLoggedIn: false,
  login: () => {},
  logout: () => {},
})

const DOCTOR_DEFAULT: UserData = {
  name: "Dr. Calvin Carlo",
  email: "calvin@example.com",
  role: "doctor",
  avatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=100&h=100&fit=crop&crop=face",
  specialty: "Orthopedic",
}

const PATIENT_DEFAULT: UserData = {
  name: "Christopher Burrell",
  email: "christopher@example.com",
  role: "patient",
  avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=200&h=200&fit=crop&crop=face",
  age: "25 Years old",
}

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<UserData | null>(null)
  const [mounted, setMounted] = useState(false)

  useEffect(() => {
    setMounted(true)
    try {
      const stored = localStorage.getItem("medicare_user")
      if (stored) {
        setUser(JSON.parse(stored))
      }
    } catch {
      // ignore
    }
  }, [])

  const login = useCallback((role: UserRole, data?: Partial<UserData>) => {
    const defaults = role === "doctor" ? DOCTOR_DEFAULT : PATIENT_DEFAULT
    const userData = { ...defaults, ...data, role } as UserData
    setUser(userData)
    localStorage.setItem("medicare_user", JSON.stringify(userData))
  }, [])

  const logout = useCallback(() => {
    setUser(null)
    localStorage.removeItem("medicare_user")
  }, [])

  if (!mounted) {
    return <>{children}</>
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        role: user?.role ?? null,
        isLoggedIn: !!user,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  return useContext(AuthContext)
}
