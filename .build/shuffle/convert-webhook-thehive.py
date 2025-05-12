import json

# Get webhook data from the previous node
nodevalue = r"""$exec"""
if not nodevalue:
    nodevalue = r"""{"sample": "string", "int": 1}"""
data = json.loads(nodevalue)

# Handle context_alerts safely (can be dict or stringified JSON)
context_alerts = data.get("context_alerts", {})
if isinstance(context_alerts, str):
    try:
        context_alerts = json.loads(context_alerts)
    except Exception:
        context_alerts = {}

# Handle nested field layer 1
signal = context_alerts.get("signal", {}) if isinstance(context_alerts, dict) else {}
kibana = context_alerts.get("kibana", {}) if isinstance(context_alerts, dict) else {}
host   = context_alerts.get("host", {}) if isinstance(context_alerts, dict) else {}
user   = context_alerts.get("user", {}) if isinstance(context_alerts, dict) else {}

#Handle nested field layer 2 
alert = kibana.get("alert", {}) if isinstance(kibana, dict) else {}

#Handle nested field layer 3
rule = signal.get("rule", {}) if isinstance(alert, dict) else {}

# Severity mapping from string to number
severity_mapping = {
    "low": 1,
    "medium": 2,
    "high": 3,
    "critical": 4,
}
severity_str = alert.get("severity", "low").lower()
severity = severity_mapping.get(severity_str, 1)

# Basic field extraction
summary = signal.get("reason", "No summary")
rule_name = data.get("rule_name", "No title")

# Description template field
hostname = host.get("hostname", "Unknown")
risk_score = data.get("risk_score", "none")

#Title
title = f"Malicious activities raise {rule_name} on {hostname} using by `{user.get('name','Unknown')}`"

#Desc 
description = (
    f"### Alert Summary\n"
    f"- **Source**: SOCIMP Elastic SIEM\n"
    f"- **Infected Host-Machine**: `{hostname}`\n"
    f"- **Infected Username**: `{user.get('name','Unknown')}`\n"
    f"- **Infected IP Address**: `{host.get('ip','Unknown')}`\n"
    f"- **Severity**: ({severity_str})\n"
    f"- **Risk Score**: `{rule.get('risk_score','Unknown')}`\n"
    f"- **Details**: {signal.get('reason', 'No summary')}\n"
)

# Tags from strings to array 
rule_tags = data.get("rule_tags", "")
tag_list = [tag.strip() for tag in rule_tags.split(",") if tag.strip()]

# Result payload match with TheHive Alert body
result = {
    "description": description,
    "externallink": "SOCIMP",
    "pap": 3,
    "severity": severity,
    "source": "SOCIMP Elastic SIEM",
    "sourceRef": data.get("date", ""),     
    "status": "New",
    "summary": summary,
    "tags": tag_list,
    "title": title,
    "tlp": 2,
    "type": "internal"
}

# Print output as JSON in message for the next node
print(json.dumps(result, indent=2))