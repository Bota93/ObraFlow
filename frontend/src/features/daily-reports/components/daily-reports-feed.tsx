import { useDailyReports } from '../hooks/use-daily-reports'

export function DailyReportsFeed() {
  const { data, error, isLoading, isError, refetch, isFetching } =
    useDailyReports()

  if (isLoading) {
    return (
      <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <div className="space-y-1">
          <div className="h-4 w-36 animate-pulse rounded bg-slate-200" />
          <div className="h-8 w-40 animate-pulse rounded bg-slate-200" />
        </div>
        <div className="mt-6 grid gap-4">
          {Array.from({ length: 3 }).map((_, index) => (
            <div
              key={index}
              className="h-24 animate-pulse rounded-2xl bg-slate-100"
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
              Daily reports unavailable
            </p>
            <p className="text-sm leading-6">
              {error?.message ??
                'Daily report data could not be loaded from the API.'}
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
            Daily reports
          </p>
          <h2 className="text-2xl font-semibold tracking-tight text-slate-950">
            No reports yet
          </h2>
          <p className="text-sm leading-6 text-slate-600">
            Daily site activity will appear here once reports are created in the
            system.
          </p>
        </div>
      </section>
    )
  }

  const formatReportDate = (value: string) => {
    const match = /^(\d{4})-(\d{2})-(\d{2})$/.exec(value)
    if (!match) {
      return value
    }

    const year = Number(match[1])
    const month = Number(match[2])
    const day = Number(match[3])
    const date = new Date(Date.UTC(year, month - 1, day))

    if (
      Number.isNaN(date.getTime()) ||
      date.getUTCFullYear() !== year ||
      date.getUTCMonth() !== month - 1 ||
      date.getUTCDate() !== day
    ) {
      return value
    }

    return new Intl.DateTimeFormat('en-US', {
      dateStyle: 'medium',
      timeZone: 'UTC',
    }).format(date)
  }

  const formatHoursWorked = (value: number) =>
    new Intl.NumberFormat('en-US', {
      minimumFractionDigits: 1,
      maximumFractionDigits: 1,
    }).format(value)

  return (
    <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <div className="space-y-1">
        <p className="text-sm font-semibold uppercase tracking-[0.2em] text-slate-500">
          Site reporting
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
              <div className="rounded-xl bg-white px-3 py-2 text-sm text-slate-500 shadow-sm">
                <p className="text-[11px] font-medium uppercase tracking-[0.12em] text-slate-400">
                  Report date
                </p>
                <p>{formatReportDate(report.date)}</p>
                <p className="mt-2 text-[11px] font-medium uppercase tracking-[0.12em] text-slate-400">
                  Hours worked
                </p>
                <p>{formatHoursWorked(report.hoursWorked)} h</p>
              </div>
            </div>
          </article>
        ))}
      </div>
    </section>
  )
}
