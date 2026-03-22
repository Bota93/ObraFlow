import { Outlet } from 'react-router-dom'

export function PublicLayout() {
  return (
    <div className="min-h-screen bg-slate-950 px-6 py-12 text-slate-100">
      <div className="mx-auto max-w-4xl">
        <Outlet />
      </div>
    </div>
  )
}
