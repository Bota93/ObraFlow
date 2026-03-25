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

Important settings already prepared there:

- Docker runtime from `backend/`
- health check on `GET /health`
- forwarded headers enabled for reverse-proxy HTTPS handling
- startup migrations enabled

Set these environment variables in Render:

- `ConnectionStrings__DefaultConnection`
  - secret
  - set this to the Supabase Session pooler connection string
- `Cors__AllowedOrigins`
  - not secret, but environment-specific
  - comma-separated list such as `https://obraflow.vercel.app,https://app.example.com`

Avoid broad wildcards such as `https://*.vercel.app` unless you explicitly want to trust every matching preview subdomain pattern.

`Database__ApplyMigrationsOnStartup=true` is convenient for a single-instance deployment. If you later scale the API horizontally, prefer a controlled migration step before rollout.

## Vercel

Create the Vercel project with:

- Root Directory: `frontend`

The frontend includes `frontend/vercel.json` for:

- Vite build settings
- SPA rewrites to `index.html`

Set only this frontend environment variable:

- `VITE_API_BASE_URL=https://your-render-service.onrender.com`

Important:

- Vite exposes `VITE_*` values to the browser
- never place secrets, keys, database URLs, or privileged tokens in `VITE_*`

## Recommended Rollout Order

1. Create the Supabase project.
2. Copy the Session pooler connection string into Render as `ConnectionStrings__DefaultConnection`.
3. Deploy the API on Render from `render.yaml`.
4. Set `Cors__AllowedOrigins` in Render to the final Vercel domain.
5. Deploy the frontend on Vercel with `VITE_API_BASE_URL` pointing to Render.
6. After Vercel gives you the production URL, confirm it is present in the Render CORS list.

## Local Files

Example environment files are included for onboarding:

- `backend/.env.example`
- `frontend/.env.example`

Create local copies as needed, but never commit the real `.env` files.
