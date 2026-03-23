import { useWorkers } from '../hooks/use-workers'

export function WorkersTable() {
  const { data, isLoading, isError } = useWorkers()

  if (isLoading) {
    return (
      <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <p className="text-sm text-slate-500">Loading workers...</p>
      </section>
    )
  }

  if (isError || !data) {
    return (
      <section className="rounded-2xl border border-rose-200 bg-rose-50 p-6 text-sm text-rose-700 shadow-sm">
        Workers could not be loaded from the API.
      </section>
    )
  }

  return (
    <section className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
      <div className="border-b border-slate-200 px-6 py-4">
        <p className="text-sm font-semibold uppercase tracking-[0.2em] text-slate-500">
          Workers Module
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
                <td className="px-6 py-4">{worker.hourlyRate.toFixed(2)}</td>
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
