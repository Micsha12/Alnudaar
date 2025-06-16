# Alnudaar Parental Control System

## Overview

This project consists of two main applications:

- **Alnudaar Parent App**:  
  A web-based dashboard (React frontend + .NET backend) for parents to register devices, set screen time schedules, manage website/app blocking rules, and view app usage reports.

- **Alnudaar Child App**:  
  A Windows background service that enforces screen time and blocking rules, tracks app usage, and synchronizes data with the backend.

## How the Apps Work Together

1. **Parent registers and manages devices** via the web dashboard.
2. **Child app** runs on the child's Windows device, fetching rules and schedules from the backend and uploading usage reports.
3. **Backend API** (ASP.NET Core) acts as the central hub, storing all data and serving both the frontend and the child app.

## How to Start

### 1. Start the Backend API

```sh
cd Alnudaar_parent/Alnudaar2.Server
dotnet run
```
- The backend will listen on `https://localhost:7200` by default.

### 2. Start the React Frontend

```sh
cd Alnudaar_parent/alnudaar2.client
npm install
npm run dev
```
- The frontend will run on `https://localhost:5173` by default.

### 3. Start the Child App

```sh
cd Alnudaar_Child/Alnudaar_ChildControlApp
dotnet run
```
- The child app will prompt for a device name on first run, then operate in the background.

## Notes

- Make sure the backend is running before starting the frontend or child app.
- The database will be created automatically on first run if it does not exist.
- All configuration files are in each app's folder (`appsettings.json` for .NET, `.env` or `vite.config.js` for React).
