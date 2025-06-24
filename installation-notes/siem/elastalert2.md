<h1 align="center">

ElastAlert2 Installation Notes

</h1>

ElastAlert2 is an **important** component in my project. I consider it a **"SOAR gateway"**, this lightweight software is responsible for taking Elastic Security events from Elastic SIEM and sending them to my SOAR for incident handling. This is especially useful when you’re using the basic tier of Elastic Stack, which does not support advanced response types.

### ElastAlert2 Setup
There are a few ways to deploy ElastAlert2: as a Docker container, K8S deployment or as a Python package.

Since I’m deploying it on a Proxmox LXC, i chose the Python package for better resource optimization.

**Requirements**
- Elasticsearch 7.x or 8.x, or OpenSearch 1.x or 2.x
- ISO8601 or Unix timestamped data
- Python 3.13 (requires OpenSSL 3.0.8 or newer). Note: Python 3.12 is still supported but will be removed in a future release.
- pip
- On Ubuntu 24.04: build-essential, python3-pip, python3.13, python3.13-dev, libffi-dev, libssl-dev

1. Install Python 3.12
```bash
sudo add-apt-repository ppa:deadsnakes/ppa -y
sudo add-apt-repository ppa:deadsnakes/nightly -y
sudo apt update
sudo apt install python3.12 -y
```

2. Install build-essential and dependencies
```bash
sudo apt install python3-pip python3.13 python3.13-dev libffi-dev libssl-dev
```

3. Install pip
```bash
sudo apt install pip
```

4. Install ElastAlert2 package using pip
```bash
pip install elastalert2 --break-system-package
```

### Run ElastAlert2 as a systemd service
1. Create the systemd service file
```bash
sudo nano /etc/systemd/system/elastalert2.service
```

2. Example service file
This simple service is based on Taylor Walton’s example, with some adjustments for my lab environment:

```ini
[Unit]
Description=Elastalert2 Systemd Service
After=syslog.target
After=network.target

[Service]
Type=simple
User=root
Group=root
WorkingDirectory=/opt/elastalert2
Environment="SOME_KEY_1=value" "SOME_KEY_2=value2"
Restart=always
ExecStart=/usr/bin/python3 -m elastalert.elastalert --verbose --rule /opt/elastalert2/rules/http/http_post.yaml --config config.yaml
TimeoutSec=60

[Install]
WantedBy=multi-user.target
```

3. Reload the systemd daemon
```bash
sudo systemctl reload-daemon
```

Now, with the proper config and rule files, you can start ElastAlert2 as a service using:
```bash
sudo systemctl start elastalert2.service
```
This is much easier than running `python -m elastalert.elastalert --verbose --rule example_rule.yaml --config config.yaml` every time.

In the SIEM deployment section, I’ll discuss configuration and rule creation, as these are key parts of any SIEM implementation.

### Useful Info / Important Note
For example config and rule files, see the [ElastAlert2 Repo](https://github.com/jertel/elastalert2)