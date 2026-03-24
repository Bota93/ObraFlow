export function DemoNotice() {
  return (
    <div className="rounded-xl border border-amber-200 bg-amber-50 px-4 py-3 text-sm text-amber-950">
      <p className="font-medium">Public demo environment</p>
      <p className="mt-1 text-amber-900/90">
        Demo data may reset periodically. Please avoid entering real or
        sensitive information.
      </p>
    </div>
  )
}
