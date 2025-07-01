
# Phase 4 - Elastic Security Alerting

## Overview

Now that detection rules are running in Elastic SIEM, it's time to focus on **getting those alerts into the right hands**. In this phase, I set up a reliable alerting mechanism that forwards security alerts directly to my SOAR workflows using ElastAlert2 and n8n.

## Alerting Workflow

The alerting flow is simple and effective:

```
Detection Rule → Signal Generated → ElastAlert2 → Webhook to n8n → Triage & Notification
```

## Setting Up ElastAlert2

To connect Elastic SIEM with n8n, I used **ElastAlert2**-a flexible alerting engine that queries detection signals and forwards them to external systems.

### 1. Main Configuration (`config.yaml`)

```yaml
rules_folder: opt/elastalert2/rules
run_every:
  seconds: 30
buffer_time:
  minutes: 3

es_host: <elasticsearch_host>
es_port: 9200
use_ssl: True
verify_certs: True
ssl_show_warn: True

es_username: <elasticsearch_username>
es_password: <elasticsearch_password>

ca_certs: /opt/elastalert2/cert/http_ca.crt

writeback_index: elastalert_status

alert_time_limit:
  days: 2
```

This setup checks for new alerts every 30 seconds and connects securely to the Elastic instance.

### 2. Alert Rule (`http_post.yaml`)

```yaml
# Forwards all Elastic alerts to n8n via HTTP POST.
# Deduplicates alerts from the same host within 5 seconds to reduce noise.

name: "Forward All Elastic Alerts to Shuffle"
index: .internal.alerts-security.alerts-default-*
type: any

query_key: host.name
realert:
  seconds: 5

alert:
  - post2

http_post2_url: "<n8n_webhook_url>"
http_post2_ignore_ssl_errors: True
http_post2_all_values: True

headers:
  Content-Type: "application/json"
```

This rule sends the complete alert context to a webhook in n8n, where automation takes over-enriching data, creating tickets, and notifying analysts.

### 3. Running as a Service (`elastalert2.service`)

```ini
[Unit]
Description=ElastAlert2 Systemd Service
After=syslog.target
After=network.target

[Service]
Type=simple
User=root
Group=root
WorkingDirectory=/opt/elastalert2
Environment="SOME_KEY_1=value" "SOME_KEY_2=value2"
Restart=always
ExecStart=/usr/bin/python3 -m elastalert.elastalert   --verbose   --rule /opt/elastalert2/rules/http/http_post.yaml   --config config.yaml
TimeoutSec=60

[Install]
WantedBy=multi-user.target
```

By running ElastAlert2 as a systemd service, I ensure it's always up and alerting-even after reboots.

---

## What Happens Next

Once everything is in place:

- Detection rules trigger signals in Elastic Security
- ElastAlert2 picks them up and pushes alerts to the n8n webhook
- n8n handles triage, IOC enrichment, and notifications (Slack, email, TheHive, etc.)

This setup keeps detection and response decoupled-so I can scale and update each part independently.

