export function IncidentsPlaceholder() {
  return (
    <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <p className="text-sm font-semibold uppercase tracking-[0.2em] text-slate-500">
        Incidents Module
      </p>
      <h2 className="mt-2 text-2xl font-semibold tracking-tight text-slate-950">
        Incident management entry point
      </h2>
      <p className="mt-3 max-w-2xl text-sm leading-6 text-slate-600">
        This slice is ready to receive query hooks, mutation flows, and UI states
        for incident tracking and updates.
      </p>
    </section>
  )
}
