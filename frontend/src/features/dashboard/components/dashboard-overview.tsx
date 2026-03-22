const dashboardMetrics = [
  { label: 'Active workers', value: '24' },
  { label: 'Reports today', value: '12' },
  { label: 'Open incidents', value: '3' },
  { label: 'Materials low stock', value: '2' },
]

export function DashboardOverview() {
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
