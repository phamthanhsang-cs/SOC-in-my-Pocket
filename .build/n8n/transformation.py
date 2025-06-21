# Severity mapping
severity_map = {
    'low': 1,
    'medium': 2,
    'high': 3,
    'critical': 4
}

# Get data from ElastAlert
body = _input.first().json.get("body", {})

# Severity
severity = body.get("kibana.alert.rule.severity", "")
severity_numeric = severity_map.get(severity.lower(), 0)

# TheHive Alert fields
description = body.get("kibana.alert.reason", "No description provided")
title = body.get("kibana.alert.rule.name", "No title provided")
host = body.get("host", {})
risk_score = body.get("kibana.alert.rule.risk_score", "none")

summary = (
    f"### Alert Summary\n"
    f"- **Source**: SOCIMP Elastic Security\n"
    f"- **Infected Host-Machine**: `{host.get('hostname','Unknown')}`\n"
    f"- **Infected Username**: `{host.get('name','Unknown')}`\n"
    f"- **Infected IP Address**: `{host.get('ip','Unknown')}`\n"
    f"- **Severity**: {severity}\n"
    f"- **Risk Score**: `{risk_score}`\n"
    f"- **Details**: {body.get('kibana.alert.rule.description', 'No summary')}\n"
    f"- **References**: {body.get('kibana.alert.rule.references', [])}\n"
)

# Build observables list
observables = []

def add_observable(obs_type, value):
    if value and value.lower() != "unknow":
        observables.append({ "json": { "type": obs_type, "value": value } })

# Add observables
add_observable("ip", body.get("source", {}).get("ip", "Unknow"))
add_observable("domain", body.get("url", {}).get("domain", "Unknow"))
add_observable("filename", body.get("process", {}).get("pe", {}).get("original_file_name", "Unknow"))
add_observable("other", body.get("process", {}).get("command_line", "Unknow"))

# Hashes → all as type "hash"
hashes = body.get("process", {}).get("hash", {})
for h in ["md5", "sha1", "sha256"]:
    hash_value = hashes.get(h, "Unknow")
    add_observable("hash", hash_value)

# Final output
return [
    {
        "json": {
            "severity_numeric": severity_numeric,
            "description": description,
            "title": title,
            "summary": summary,
            "observables": observables  
        }
    }
]
