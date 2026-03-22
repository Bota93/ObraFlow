type PageHeaderProps = {
  eyebrow: string
  title: string
  description: string
}

export function PageHeader({
  eyebrow,
  title,
  description,
}: PageHeaderProps) {
  return (
    <header className="space-y-3">
      <p className="text-xs font-semibold uppercase tracking-[0.24em] text-slate-500">
        {eyebrow}
      </p>
      <div className="space-y-2">
        <h1 className="text-3xl font-semibold tracking-tight text-slate-950 sm:text-4xl">
          {title}
        </h1>
        <p className="max-w-3xl text-sm leading-6 text-slate-600 sm:text-base">
          {description}
        </p>
      </div>
    </header>
  )
}
