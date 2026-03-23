import { useDashboardSummary } from '../hooks/use-dashboard-summary'

export function DashboardMetrics() {
  const { data, error, isLoading, isError, refetch, isFetching } =
    useDashboardSummary()

  const isEmpty =
    !!data &&
    data.totalWorkers === 0 &&
    data.activeWorkers === 0 &&
    data.totalDailyReports === 0 &&
    data.openIncidents === 0 &&
    data.inProgressIncidents === 0 &&
    data.resolvedIncidents === 0 &&
    data.hoursWorkedToday === 0 &&
    data.hoursWorkedLast7Days === 0

  if (isLoading) {
    return (
      <section className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
        {Array.from({ length: 8 }).map((_, index) => (
          <div
            key={index}
            className="h-32 animate-pulse rounded-2xl border border-slate-200 bg-white shadow-sm"
          />
        ))}
      </section>
    )
  }

  if (isError) {
    return (
      <section className="rounded-2xl border border-rose-200 bg-rose-50 p-6 text-rose-800 shadow-sm">
        <div className="space-y-4">
          <div className="space-y-1">
            <p className="text-sm font-semibold uppercase tracking-[0.18em] text-rose-700">
              Dashboard unavailable
            </p>
            <p className="text-sm leading-6">
              {error?.message ??
                'Dashboard metrics could not be loaded from the API.'}
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

  if (!data || isEmpty) {
    return (
      <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <div className="space-y-1">
          <p className="text-sm font-semibold uppercase tracking-[0.18em] text-slate-500">
            Dashboard ready
          </p>
          <p className="text-sm leading-6 text-slate-600">
            No operational activity is available yet. Metrics will appear here
            as workers, reports, and incidents are created.
          </p>
        </div>
      </section>
    )
  }

  const formatHours = (value: number) =>
    new Intl.NumberFormat('en-US', {
      minimumFractionDigits: 1,
      maximumFractionDigits: 1,
    }).format(value)

  const dashboardMetrics = [
    {
      label: 'Total workers',
      value: String(data.totalWorkers),
      accentClass: 'bg-slate-100 text-slate-700',
    },
    {
      label: 'Active workers',
      value: String(data.activeWorkers),
      accentClass: 'bg-emerald-100 text-emerald-700',
    },
    {
      label: 'Reports today',
      value: String(data.totalDailyReports),
      accentClass: 'bg-sky-100 text-sky-700',
    },
    {
      label: 'Open incidents',
      value: String(data.openIncidents),
      accentClass: 'bg-rose-100 text-rose-700',
    },
    {
      label: 'In progress incidents',
      value: String(data.inProgressIncidents),
      accentClass: 'bg-amber-100 text-amber-700',
    },
    {
      label: 'Resolved incidents',
      value: String(data.resolvedIncidents),
      accentClass: 'bg-emerald-100 text-emerald-700',
    },
    {
      label: 'Hours today',
      value: formatHours(data.hoursWorkedToday),
      accentClass: 'bg-violet-100 text-violet-700',
      suffix: 'h',
    },
    {
      label: 'Hours last 7 days',
      value: formatHours(data.hoursWorkedLast7Days),
      accentClass: 'bg-indigo-100 text-indigo-700',
      suffix: 'h',
    },
  ]

  return (
    <section className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
      {dashboardMetrics.map((metric) => (
        <article
          key={metric.label}
          className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm transition hover:-translate-y-0.5 hover:shadow-md"
        >
          <div className="flex items-start justify-between gap-3">
            <p className="text-sm font-medium text-slate-500">{metric.label}</p>
            <span
              className={[
                'inline-flex rounded-full px-2.5 py-1 text-xs font-semibold',
                metric.accentClass,
              ].join(' ')}
            >
              Live
            </span>
          </div>
          <p className="mt-4 text-3xl font-semibold tracking-tight text-slate-950">
            {metric.value}
            {metric.suffix ? (
              <span className="ml-1 text-lg font-medium text-slate-500">
                {metric.suffix}
              </span>
            ) : null}
          </p>
        </article>
      ))}
    </section>
  )
}
