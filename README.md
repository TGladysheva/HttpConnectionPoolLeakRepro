# HttpConnectionPoolLeakRepro

## Server
  Listening for 10 different ports on localhost

## Client
  Creates HttpClient with default configuration and sends one request per server's port

## Log
  Logs events from `Private.InternalDiagnostics.System.Net.Http` and `System.Net` event sources

## Dump
  Created after 5 minutes delay from the last request from client
