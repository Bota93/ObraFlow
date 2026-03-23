import { useWorkers } from '../hooks/use-workers'

export function WorkersTable() {
  const { data, error, isLoading, isError, refetch, isFetching } = useWorkers()

  if (isLoading) {
    return (
      <section className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
        <div className="border-b border-slate-200 px-6 py-4">
          <div className="h-4 w-32 animate-pulse rounded bg-slate-200" />
          <div className="mt-3 h-8 w-48 animate-pulse rounded bg-slate-200" />
        </div>
        <div className="space-y-3 px-6 py-4">
          {Array.from({ length: 5 }).map((_, index) => (
            <div
              key={index}
              className="h-12 animate-pulse rounded-xl bg-slate-100"
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
              Workers unavailable
            </p>
            <p className="text-sm leading-6">
              {error?.message ?? 'Workers data could not be loaded from the API.'}
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
            Workers
          </p>
          <h2 className="text-2xl font-semibold tracking-tight text-slate-950">
            No workers yet
          </h2>
          <p className="text-sm leading-6 text-slate-600">
            The workforce list is empty. Workers will appear here once they are
            created in the system.
          </p>
        </div>
      </section>
    )
  }

  const formatHourlyRate = (value: number) =>
    new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    }).format(value)

  return (
    <section className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
      <div className="border-b border-slate-200 px-6 py-4">
        <p className="text-sm font-semibold uppercase tracking-[0.2em] text-slate-500">
          Workforce
        </p>
        <h2 className="mt-2 text-2xl font-semibold tracking-tight text-slate-950">
          Current workforce
        </h2>
      </div>
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-slate-200 text-left">
          <thead className="bg-slate-50 text-xs font-semibold uppercase tracking-[0.16em] text-slate-500">
            <tr>
              <th className="px-6 py-4">Name</th>
              <th className="px-6 py-4">Role</th>
              <th className="px-6 py-4">Phone</th>
              <th className="px-6 py-4">Rate</th>
              <th className="px-6 py-4">Status</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-200">
            {data.map((worker) => (
              <tr key={worker.id} className="text-sm text-slate-700">
                <td className="px-6 py-4 font-medium text-slate-950">
                  {worker.name}
                </td>
                <td className="px-6 py-4">{worker.role}</td>
                <td className="px-6 py-4">{worker.phoneNumber}</td>
                <td className="px-6 py-4">
                  {formatHourlyRate(worker.hourlyRate)} / hr
                </td>
                <td className="px-6 py-4">
                  <span
                    className={[
                      'inline-flex rounded-full px-3 py-1 text-xs font-semibold',
                      worker.isActive
                        ? 'bg-emerald-100 text-emerald-700'
                        : 'bg-slate-200 text-slate-700',
                    ].join(' ')}
                  >
                    {worker.isActive ? 'Active' : 'Inactive'}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  )
}
