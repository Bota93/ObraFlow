import { useDashboardSummary } from '../hooks/use-dashboard-summary'

export function DashboardOverview() {
  const { data, isLoading, isError } = useDashboardSummary()

  if (isLoading) {
    return (
      <section className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
        {Array.from({ length: 4 }).map((_, index) => (
          <div
            key={index}
            className="h-32 animate-pulse rounded-2xl border border-slate-200 bg-white"
          />
        ))}
      </section>
    )
  }

  if (isError || !data) {
    return (
      <section className="rounded-2xl border border-rose-200 bg-rose-50 p-6 text-sm text-rose-700">
        Dashboard data could not be loaded from the API.
      </section>
    )
  }

  const dashboardMetrics = [
    { label: 'Total workers', value: String(data.totalWorkers) },
    { label: 'Active workers', value: String(data.activeWorkers) },
    { label: 'Reports today', value: String(data.totalDailyReports) },
    { label: 'Open incidents', value: String(data.openIncidents) },
    {
      label: 'In progress incidents',
      value: String(data.inProgressIncidents),
    },
    { label: 'Resolved incidents', value: String(data.resolvedIncidents) },
    { label: 'Hours today', value: String(data.hoursWorkedToday) },
    { label: 'Hours last 7 days', value: String(data.hoursWorkedLast7Days) },
  ]

  return (
    <section className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
      {dashboardMetrics.map((metric) => (
        <article
          key={metric.label}
          className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm"
        >
          <p className="text-sm font-medium text-slate-500">{metric.label}</p>
          <p className="mt-3 text-3xl font-semibold tracking-tight text-slate-950">
            {metric.value}
          </p>
        </article>
      ))}
    </section>
  )
}
