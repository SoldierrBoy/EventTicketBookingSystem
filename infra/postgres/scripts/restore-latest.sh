#!/bin/bash
set -e

LATEST_BACKUP=$(ls -t /backups/*.sql 2>/dev/null | head -1)

if [ -n "$LATEST_BACKUP" ]; then
    echo "Found backup: $LATEST_BACKUP. Restoring..."
    psql -U "$POSTGRES_USER" -d "$POSTGRES_DB" < "$LATEST_BACKUP"
    echo "Restore finished successfully."
else
    echo "No backup files found in /backups. Starting with empty database."
fi