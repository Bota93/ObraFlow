import { useIncidents } from '../hooks/use-incidents'

const statusLabels: Record<number, string> = {
  1: 'Open',
  2: 'In progress',
  3: 'Resolved',
}

export function IncidentsFeed() {
  const { data, error, isLoading, isError, refetch, isFetching } =
    useIncidents()

  if (isLoading) {
    return (
      <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <div className="space-y-1">
          <div className="h-4 w-28 animate-pulse rounded bg-slate-200" />
          <div className="h-8 w-44 animate-pulse rounded bg-slate-200" />
        </div>
        <div className="mt-6 grid gap-4">
          {Array.from({ length: 3 }).map((_, index) => (
            <div
              key={index}
              className="h-28 animate-pulse rounded-2xl bg-slate-100"
            />
          ))}
        </div>
      </section>
    )
  }

  if (isError) {
    return (
      <section className="rounded-2xl border border-rose-200 bg-rose-50 p-6 text-rose-800 shadow-sm">
        <div className="space-y-4">
          <div className="space-y-1">
            <p className="text-sm font-semibold uppercase tracking-[0.18em] text-rose-700">
              Incidents unavailable
            </p>
            <p className="text-sm leading-6">
              {error?.message ??
                'Incident data could not be loaded from the API.'}
            </p>
          </div>
          <button
            type="button"
            onClick={() => void refetch()}
            disabled={isFetching}
            className="inline-flex rounded-full border border-rose-300 bg-white px-4 py-2 text-sm font-semibold text-rose-700 transition hover:bg-rose-100 disabled:cursor-not-allowed disabled:opacity-70"
          >
            {isFetching ? 'Retrying...' : 'Retry'}
          </button>
        </div>
      </section>
    )
  }

  if (!data || data.length === 0) {
    return (
      <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <div className="space-y-1">
          <p className="text-sm font-semibold uppercase tracking-[0.18em] text-slate-500">
            Incidents
          </p>
          <h2 className="text-2xl font-semibold tracking-tight text-slate-950">
            No incidents recorded
          </h2>
          <p className="text-sm leading-6 text-slate-600">
            Incident activity will appear here once reports are registered in
            the system.
          </p>
        </div>
      </section>
    )
  }

  const formatReportedAt = (value: string) => {
    const date = new Date(value)

    if (Number.isNaN(date.getTime())) {
      return value
    }

    return new Intl.DateTimeFormat('en-US', {
      dateStyle: 'medium',
      timeStyle: 'short',
    }).format(date)
  }

  return (
    <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <div className="space-y-1">
        <p className="text-sm font-semibold uppercase tracking-[0.2em] text-slate-500">
          Incident activity
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
                <p className="text-xs text-slate-500">
                  {formatReportedAt(incident.reportedAtUtc)}
                </p>
              </div>
            </div>
          </article>
        ))}
      </div>
    </section>
  )
}
