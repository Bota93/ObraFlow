import { useDailyReports } from '../hooks/use-daily-reports'

export function DailyReportsFeed() {
  const { data, isLoading, isError } = useDailyReports()

  if (isLoading) {
    return (
      <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <p className="text-sm text-slate-500">Loading daily reports...</p>
      </section>
    )
  }

  if (isError || !data) {
    return (
      <section className="rounded-2xl border border-rose-200 bg-rose-50 p-6 text-sm text-rose-700 shadow-sm">
        Daily reports could not be loaded from the API.
      </section>
    )
  }

  return (
    <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <div className="space-y-1">
        <p className="text-sm font-semibold uppercase tracking-[0.2em] text-slate-500">
          Daily Reports Module
        </p>
        <h2 className="text-2xl font-semibold tracking-tight text-slate-950">
          Latest reports
        </h2>
      </div>
      <div className="mt-6 grid gap-4">
        {data.map((report) => (
          <article
            key={report.id}
            className="rounded-2xl border border-slate-200 bg-slate-50 p-4"
          >
            <div className="flex flex-col gap-2 md:flex-row md:items-center md:justify-between">
              <div>
                <p className="text-sm font-semibold text-slate-950">
                  {report.workerName}
                </p>
                <p className="text-sm text-slate-600">{report.description}</p>
              </div>
              <div className="text-sm text-slate-500">
                <p>{report.date}</p>
                <p>{report.hoursWorked} hours</p>
              </div>
            </div>
          </article>
        ))}
      </div>
    </section>
  )
}
