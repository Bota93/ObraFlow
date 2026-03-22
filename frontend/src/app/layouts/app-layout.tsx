import { NavLink, Outlet } from 'react-router-dom'
import { navigationItems } from '../../shared/constants/navigation'

export function AppLayout() {
  return (
    <div className="min-h-screen bg-slate-100 text-slate-900">
      <div className="mx-auto flex min-h-screen max-w-7xl flex-col lg:flex-row">
        <aside className="border-b border-slate-200 bg-slate-950 px-6 py-6 text-slate-100 lg:min-h-screen lg:w-72 lg:border-b-0 lg:border-r lg:px-8">
          <div className="space-y-2">
            <p className="text-xs font-semibold uppercase tracking-[0.3em] text-amber-300">
              ObraFlow
            </p>
            <h1 className="text-2xl font-semibold tracking-tight">
              Construction Operations
            </h1>
            <p className="max-w-xs text-sm leading-6 text-slate-300">
              Internal workspace for site reporting, incidents, workforce, and
              operational visibility.
            </p>
          </div>

          <nav className="mt-8 flex flex-col gap-2">
            {navigationItems.map((item) => (
              <NavLink
                key={item.to}
                to={item.to}
                className={({ isActive }) =>
                  [
                    'rounded-xl px-4 py-3 text-sm font-medium transition',
                    isActive
                      ? 'bg-amber-300 text-slate-950'
                      : 'text-slate-200 hover:bg-slate-800 hover:text-white',
                  ].join(' ')
                }
              >
                {item.label}
              </NavLink>
            ))}
          </nav>
        </aside>

        <div className="flex min-h-screen flex-1 flex-col">
          <header className="border-b border-slate-200 bg-white/90 px-6 py-4 backdrop-blur lg:px-10">
            <div className="flex flex-col gap-1">
              <p className="text-xs font-semibold uppercase tracking-[0.24em] text-slate-500">
                MVP Frontend Foundation
              </p>
              <p className="text-sm text-slate-600">
                Architecture-first scaffold aligned with the backend modules.
              </p>
            </div>
          </header>

          <main className="flex-1 px-6 py-8 lg:px-10">
            <Outlet />
          </main>
        </div>
      </div>
    </div>
  )
}
