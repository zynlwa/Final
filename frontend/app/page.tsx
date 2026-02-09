import Link from "next/link"
import {
  Heart,
  Calendar,
  Shield,
  Users,
  Star,
  ArrowRight,
  Stethoscope,
  Brain,
  Bone,
  Eye,
  Quote,
} from "lucide-react"
import Navbar from "@/components/navbar"
import Footer from "@/components/footer"

const services = [
  { name: "Cardiology", icon: Heart, desc: "Heart and cardiovascular care" },
  { name: "Neurology", icon: Brain, desc: "Brain and nervous system" },
  { name: "Orthopedic", icon: Bone, desc: "Bones and joint health" },
  { name: "Ophthalmology", icon: Eye, desc: "Eye care and vision" },
]

const doctors = [
  {
    name: "Dr. Calvin Carlo",
    specialty: "Orthopedic",
    rating: 4.9,
    slug: "dr-calvin-carlo",
    avatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=200&h=200&fit=crop&crop=face",
  },
  {
    name: "Dr. Cristino Murphy",
    specialty: "Gynecologist",
    rating: 4.8,
    slug: "dr-cristino-murphy",
    avatar: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=200&h=200&fit=crop&crop=face",
  },
  {
    name: "Dr. Alia Reddy",
    specialty: "Psychotherapist",
    rating: 4.7,
    slug: "dr-alia-reddy",
    avatar: "https://images.unsplash.com/photo-1594824476967-48c8b964ac31?w=200&h=200&fit=crop&crop=face",
  },
  {
    name: "Dr. Toni Kovar",
    specialty: "Orthopedic",
    rating: 4.8,
    slug: "dr-james-moore",
    avatar: "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=200&h=200&fit=crop&crop=face",
  },
]

const testimonials = [
  {
    name: "Christopher Burrell",
    role: "Patient",
    avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=100&h=100&fit=crop&crop=face",
    text: "The doctors at MediCare are incredibly professional and caring. My experience with Dr. Calvin was outstanding. He explained everything clearly and made me feel comfortable throughout the entire treatment process.",
    rating: 5,
  },
  {
    name: "Emily Richardson",
    role: "Patient",
    avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=100&h=100&fit=crop&crop=face",
    text: "I had a wonderful experience at MediCare. The staff was friendly, the facility was clean, and Dr. Alia Reddy took the time to listen to all my concerns. Highly recommend this clinic to everyone.",
    rating: 5,
  },
  {
    name: "Robert Johnson",
    role: "Patient",
    avatar: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=100&h=100&fit=crop&crop=face",
    text: "Booking an appointment was incredibly easy and the follow-up care was exceptional. The team genuinely cares about patient outcomes and it shows in every interaction.",
    rating: 4,
  },
]

const blogPosts = [
  {
    id: "consultant-business",
    title: "Consultant Business",
    excerpt: "This is required when, for example, the final text is not yet available. Dummy text is also known as 'fill text'.",
    image: "https://images.unsplash.com/photo-1576091160399-112ba8d25d1d?w=400&h=280&fit=crop",
    author: "Dr. Calvin Carlo",
    date: "13th Sep 2025",
  },
  {
    id: "look-on-the-glorious-balance",
    title: "Look On The Glorious Balance",
    excerpt: "Due to its widespread use as filler text for layouts, non-readability is of great importance in typesetting.",
    image: "https://images.unsplash.com/photo-1579684385127-1ef15d508118?w=400&h=280&fit=crop",
    author: "Dr. Alia Reddy",
    date: "29th Nov 2025",
  },
  {
    id: "research-financial",
    title: "Research Financial",
    excerpt: "For this reason, dummy text usually consists of a more or less random series of words or syllables.",
    image: "https://images.unsplash.com/photo-1551076805-e1869033e561?w=400&h=280&fit=crop",
    author: "Dr. Cristino Murphy",
    date: "29th Dec 2025",
  },
]

export default function HomePage() {
  return (
    <div className="min-h-screen">
      <Navbar />

      {/* Hero */}
      <section className="relative overflow-hidden bg-card">
        <div className="mx-auto flex max-w-7xl flex-col items-center gap-8 px-4 py-20 text-center lg:px-8 lg:py-28">
          <div className="flex h-14 w-14 items-center justify-center rounded-2xl bg-primary/10">
            <Stethoscope className="h-7 w-7 text-primary" />
          </div>
          <h1 className="max-w-3xl text-balance text-4xl font-bold tracking-tight text-foreground lg:text-6xl">
            Your Health, Our Priority
          </h1>
          <p className="max-w-xl text-pretty text-lg leading-relaxed text-muted-foreground">
            Book appointments with top specialists. Get quality healthcare from trusted professionals, all in one place.
          </p>
          <div className="flex flex-wrap items-center justify-center gap-4">
            <Link
              href="/book-appointment"
              className="flex items-center gap-2 rounded-xl bg-primary px-6 py-3 text-sm font-semibold text-primary-foreground transition-opacity hover:opacity-90"
            >
              Book Appointment
              <ArrowRight className="h-4 w-4" />
            </Link>
            <Link
              href="/doctors"
              className="rounded-xl border border-border bg-card px-6 py-3 text-sm font-semibold text-foreground transition-colors hover:bg-secondary"
            >
              Browse Doctors
            </Link>
          </div>

          <div className="mt-6 flex flex-wrap items-center justify-center gap-8 text-muted-foreground">
            <div className="flex items-center gap-2">
              <Shield className="h-5 w-5 text-primary" />
              <span className="text-sm">Verified Doctors</span>
            </div>
            <div className="flex items-center gap-2">
              <Calendar className="h-5 w-5 text-primary" />
              <span className="text-sm">Easy Scheduling</span>
            </div>
            <div className="flex items-center gap-2">
              <Users className="h-5 w-5 text-primary" />
              <span className="text-sm">10k+ Patients</span>
            </div>
          </div>
        </div>
      </section>

      {/* Services */}
      <section className="mx-auto max-w-7xl px-4 py-16 lg:px-8 lg:py-24">
        <div className="mb-12 text-center">
          <h2 className="text-3xl font-bold text-foreground">Our Services</h2>
          <p className="mt-3 text-muted-foreground">
            Comprehensive healthcare services for you and your family
          </p>
        </div>
        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-4">
          {services.map((service) => (
            <div
              key={service.name}
              className="group rounded-xl border border-border bg-card p-6 transition-all hover:border-primary/30 hover:shadow-md"
            >
              <div className="mb-4 flex h-12 w-12 items-center justify-center rounded-xl bg-primary/10 transition-colors group-hover:bg-primary">
                <service.icon className="h-6 w-6 text-primary transition-colors group-hover:text-primary-foreground" />
              </div>
              <h3 className="mb-1 text-lg font-semibold text-foreground">{service.name}</h3>
              <p className="text-sm text-muted-foreground">{service.desc}</p>
            </div>
          ))}
        </div>
      </section>

      {/* Doctors Preview */}
      <section className="bg-card">
        <div className="mx-auto max-w-7xl px-4 py-16 lg:px-8 lg:py-24">
          <div className="mb-12 text-center">
            <h2 className="text-3xl font-bold text-foreground">Top Doctors</h2>
            <p className="mt-3 text-muted-foreground">
              Meet our experienced healthcare professionals
            </p>
          </div>
          <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-4">
            {doctors.map((doc) => (
              <Link
                key={doc.name}
                href={`/doctors/${doc.slug}`}
                className="group flex flex-col items-center rounded-xl border border-border bg-background p-6 text-center transition-all hover:border-primary hover:shadow-lg"
              >
                <img
                  src={doc.avatar || "/placeholder.svg"}
                  alt={doc.name}
                  className="mb-4 h-32 w-32 rounded-xl object-cover transition-transform group-hover:scale-105"
                />
                <h3 className="text-lg font-semibold text-foreground group-hover:text-primary">{doc.name}</h3>
                <p className="mb-3 text-sm text-muted-foreground">{doc.specialty}</p>
                <div className="flex items-center gap-1">
                  <Star className="h-4 w-4 fill-amber-400 text-amber-400" />
                  <span className="text-sm font-medium text-foreground">{doc.rating}</span>
                </div>
              </Link>
            ))}
          </div>
          <div className="mt-10 text-center">
            <Link
              href="/doctors"
              className="inline-flex items-center gap-2 rounded-xl bg-primary px-6 py-3 text-sm font-semibold text-primary-foreground transition-opacity hover:opacity-90"
            >
              See More
              <ArrowRight className="h-4 w-4" />
            </Link>
          </div>
        </div>
      </section>

      {/* Patient Says / Testimonials */}
      <section className="mx-auto max-w-7xl px-4 py-16 lg:px-8 lg:py-24">
        <div className="mb-12 text-center">
          <h2 className="text-3xl font-bold text-foreground">Patient Says</h2>
          <p className="mt-3 text-muted-foreground">
            What our patients say about their experience with MediCare
          </p>
        </div>
        <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
          {testimonials.map((testimonial) => (
            <div
              key={testimonial.name}
              className="relative rounded-xl border border-border bg-card p-6 transition-shadow hover:shadow-md"
            >
              <Quote className="mb-4 h-8 w-8 text-primary/20" />
              <p className="mb-6 leading-relaxed text-muted-foreground">
                {testimonial.text}
              </p>
              <div className="flex items-center gap-1 mb-4">
                {Array.from({ length: 5 }).map((_, i) => (
                  <Star
                    key={i}
                    className={`h-4 w-4 ${i < testimonial.rating ? "fill-amber-400 text-amber-400" : "text-border"}`}
                  />
                ))}
              </div>
              <div className="flex items-center gap-3 border-t border-border pt-4">
                <img
                  src={testimonial.avatar || "/placeholder.svg"}
                  alt={testimonial.name}
                  className="h-11 w-11 rounded-full object-cover"
                />
                <div>
                  <h4 className="text-sm font-semibold text-foreground">{testimonial.name}</h4>
                  <p className="text-xs text-muted-foreground">{testimonial.role}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      </section>

      {/* Latest Blog */}
      <section className="bg-card">
        <div className="mx-auto max-w-7xl px-4 py-16 lg:px-8 lg:py-24">
          <div className="mb-12 text-center">
            <h2 className="text-3xl font-bold text-foreground">Latest Blog</h2>
            <p className="mt-3 text-muted-foreground">
              Health tips, news, and insights from our medical experts
            </p>
          </div>
          <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
            {blogPosts.map((post) => (
              <article
                key={post.id}
                className="overflow-hidden rounded-xl border border-border bg-background transition-shadow hover:shadow-md"
              >
                <Link href={`/blog/${post.id}`}>
                  <img src={post.image || "/placeholder.svg"} alt={post.title} className="h-48 w-full object-cover transition-transform hover:scale-105" />
                </Link>
                <div className="p-5">
                  <div className="mb-3 flex items-center gap-3 text-xs text-muted-foreground">
                    <span>{post.author}</span>
                    <span className="h-1 w-1 rounded-full bg-muted-foreground" />
                    <span>{post.date}</span>
                  </div>
                  <Link href={`/blog/${post.id}`}>
                    <h3 className="mb-2 text-lg font-semibold text-foreground transition-colors hover:text-primary">
                      {post.title}
                    </h3>
                  </Link>
                  <p className="mb-4 line-clamp-2 text-sm leading-relaxed text-muted-foreground">
                    {post.excerpt}
                  </p>
                  <Link
                    href={`/blog/${post.id}`}
                    className="inline-flex items-center gap-1.5 text-sm font-medium text-primary transition-colors hover:text-primary/80"
                  >
                    Read More
                    <ArrowRight className="h-3.5 w-3.5" />
                  </Link>
                </div>
              </article>
            ))}
          </div>
          <div className="mt-10 text-center">
            <Link
              href="/blog"
              className="inline-flex items-center gap-2 rounded-xl bg-primary px-6 py-3 text-sm font-semibold text-primary-foreground transition-opacity hover:opacity-90"
            >
              View All Posts
              <ArrowRight className="h-4 w-4" />
            </Link>
          </div>
        </div>
      </section>

      <Footer />
    </div>
  )
}
