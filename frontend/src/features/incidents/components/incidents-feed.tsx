import { useIncidents } from '../hooks/use-incidents'

const statusLabels: Record<number, string> = {
  1: 'Open',
  2: 'In progress',
  3: 'Resolved',
}

export function IncidentsFeed() {
  const { data, isLoading, isError } = useIncidents()

  if (isLoading) {
    return (
      <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <p className="text-sm text-slate-500">Loading incidents...</p>
      </section>
    )
  }

  if (isError || !data) {
    return (
      <section className="rounded-2xl border border-rose-200 bg-rose-50 p-6 text-sm text-rose-700 shadow-sm">
        Incidents could not be loaded from the API.
      </section>
    )
  }

  return (
    <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <div className="space-y-1">
        <p className="text-sm font-semibold uppercase tracking-[0.2em] text-slate-500">
          Incidents Module
        </p>
        <h2 className="text-2xl font-semibold tracking-tight text-slate-950">
          Recent incidents
        </h2>
      </div>
      <div className="mt-6 grid gap-4">
        {data.map((incident) => (
          <article
            key={incident.id}
            className="rounded-2xl border border-slate-200 bg-slate-50 p-4"
          >
            <div className="flex flex-col gap-3 md:flex-row md:items-start md:justify-between">
              <div>
                <p className="text-sm font-semibold text-slate-950">
                  {incident.title}
                </p>
                <p className="mt-1 text-sm text-slate-600">
                  {incident.description}
                </p>
              </div>
              <div className="space-y-2 text-right">
                <span className="inline-flex rounded-full bg-amber-100 px-3 py-1 text-xs font-semibold text-amber-800">
                  {statusLabels[incident.status] ?? 'Unknown'}
                </span>
                <p className="text-xs text-slate-500">{incident.reportedAtUtc}</p>
              </div>
            </div>
          </article>
        ))}
      </div>
    </section>
  )
}
