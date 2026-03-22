export function WorkersPlaceholder() {
  return (
    <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <p className="text-sm font-semibold uppercase tracking-[0.2em] text-slate-500">
        Workers Module
      </p>
      <h2 className="mt-2 text-2xl font-semibold tracking-tight text-slate-950">
        Feature foundation ready
      </h2>
      <p className="mt-3 max-w-2xl text-sm leading-6 text-slate-600">
        This page is prepared for worker listing, detail, and creation flows. API
        hooks and schemas should live inside the workers feature when the module
        is implemented.
      </p>
    </section>
  )
}
