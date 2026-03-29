# Deployment Guide

This repository is prepared for:

- Supabase as the PostgreSQL host
- Render as the ASP.NET Core API host
- Vercel as the React/Vite frontend host

## Security First

Do not commit any of the following to GitHub:

- real Supabase database passwords or connection strings
- Render or Vercel access tokens
- any `.env`, `.env.*`, `.pem`, `.key`, or exported credentials files
- any frontend `VITE_*` variable that contains a secret

Current repository audit summary:

- no tracked `.env` files were found
- no production tokens or private keys were found in the current tracked files
- the only credential-like values in the repository are local development defaults for PostgreSQL (`postgres/postgres`)

Those local defaults are not suitable for production and must be overridden with platform environment variables.

## Supabase

Use a Supabase Postgres connection string only in the backend.

Recommended for the Render runtime:

- start with the Supabase **Session pooler** connection string
- do not use the IPv6 direct host for this setup
- require SSL in the connection string
- keep the password only in Render, never in committed files

Do not use:

- the Supabase Project URL
- frontend `VITE_*` variables for database credentials

Why:

- Supabase documents the session pooler for persistent application clients and for IPv4/IPv6 compatibility
- Render documents that its network is IPv4-only

Inference:

- using the session pooler is the safest default for a long-running Render API talking to Supabase

Keep the Supabase connection string only in Render as `ConnectionStrings__DefaultConnection`.

Do not put any Supabase database credentials in:

- `frontend/.env*`
- `VITE_*` variables
- committed files

## Render

The repository root includes `render.yaml` for the API service.

Current public endpoints:

- App: `https://obraflow.adrianalcaraz.es`
- API: `https://api-obraflow.adrianalcaraz.es`
- Swagger: `https://api-obraflow.adrianalcaraz.es/swagger`

Important settings already prepared there:

- Root Directory: `backend`
- Dockerfile path: `Dockerfile`
- health check on `GET /health`
- Swagger enabled through `Swagger__Enabled=true`
- forwarded headers enabled for reverse-proxy HTTPS handling
- demo write rate limiting enabled
- startup migrations disabled by default

Set these environment variables in Render:

- `ConnectionStrings__DefaultConnection`
  - secret
  - set this to the Supabase Session pooler connection string
  - require SSL
- `Cors__AllowedOrigins`
  - prepared in `render.yaml` as `https://obraflow.adrianalcaraz.es,https://<project>.vercel.app`
  - replace `<project>` with the exact Vercel project subdomain before the final public deployment if needed
- `Swagger__Enabled`
  - set to `true` when you want the public Swagger UI available at `/swagger`

Keep `Database__ApplyMigrationsOnStartup=false` as the steady-state default. Enable it only for the initial bootstrap if the database has not been migrated yet, then set it back to `false`.

The public backend target is:

- `https://api-obraflow.adrianalcaraz.es`
- `https://api-obraflow.adrianalcaraz.es/swagger`

## Vercel

Create the Vercel project with:

- Root Directory: `frontend`

The frontend includes `frontend/vercel.json` for:

- Vite build settings
- SPA rewrites to `index.html`

Set only this frontend environment variable:

- `VITE_API_BASE_URL=https://api-obraflow.adrianalcaraz.es`

Important:

- Vite exposes `VITE_*` values to the browser
- never place secrets, keys, database URLs, or privileged tokens in `VITE_*`
- the frontend already fails fast during production build if `VITE_API_BASE_URL` is missing

The public frontend target is:

- `https://obraflow.adrianalcaraz.es`

## Recommended Rollout Order

1. Create the Supabase project.
2. Open **Connect** in Supabase and copy the **Session pooler** connection details.
3. Build the Render connection string from the pooler values with SSL required.
4. Set that string in Render as `ConnectionStrings__DefaultConnection`.
5. Set `Cors__AllowedOrigins=https://obraflow.adrianalcaraz.es,https://<project>.vercel.app` in Render if you need to replace the placeholder with the real Vercel subdomain.
6. If this is the first bootstrap, temporarily set `Database__ApplyMigrationsOnStartup=true`.
7. Deploy the API on Render from `render.yaml`.
8. After the first successful migration, set `Database__ApplyMigrationsOnStartup=false` again.
9. Deploy the frontend on Vercel with `VITE_API_BASE_URL=https://api-obraflow.adrianalcaraz.es`.
10. Confirm the custom frontend domain and the exact `https://<project>.vercel.app` domain are both covered by the backend CORS list.

## Local Files

Example environment files are included for onboarding:

- `backend/.env.example`
- `frontend/.env.example`

Create local copies as needed, but never commit the real `.env` files.
