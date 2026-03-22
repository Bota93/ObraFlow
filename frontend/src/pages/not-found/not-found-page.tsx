import { Link } from 'react-router-dom'

export function NotFoundPage() {
  return (
    <div className="flex min-h-screen items-center justify-center bg-slate-950 px-6 text-slate-100">
      <div className="max-w-md space-y-4 rounded-3xl border border-slate-800 bg-slate-900 p-8 text-center shadow-2xl shadow-slate-950/40">
        <p className="text-xs font-semibold uppercase tracking-[0.3em] text-amber-300">
          404
        </p>
        <h1 className="text-3xl font-semibold tracking-tight">Page not found</h1>
        <p className="text-sm leading-6 text-slate-300">
          The requested route does not exist in the ObraFlow frontend scaffold.
        </p>
        <Link
          to="/dashboard"
          className="inline-flex rounded-full bg-amber-300 px-5 py-3 text-sm font-semibold text-slate-950 transition hover:bg-amber-200"
        >
          Go to dashboard
        </Link>
      </div>
    </div>
  )
}
