<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/elastic-logo.png alt="logo" width="200">

Elastic SIEM Installation Notes

</h1>

Installing and configuring the **Elastic Stack** as my SIEM has been one of the most interesting parts of this project. Instead of using Docker, I chose to build the setup from scratch to experience the **node-by-node** installation process.

My current setup includes two Debian nodes: one dedicated to Elasticsearch and Kibana, and the other to the Fleet Server. If this project scales significantly and requires handling large amounts of data, I plan to expand the SIEM infrastructure to a **clustered** setup with multiple Elasticsearch and Fleet nodes for improved **high availability** and **redundancy**.

I think Elastic Team did a great job for re-visualize the workflows of the Stack so i could get more familiarize with it's components.

![Overview](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/elasticsiem/elastic-stack-overview.png)

### Setup
Since i'm deploying a single Elasticsearch node, there’s no need to expose Elasticsearch's `network.host` for security purposes; it only needs to be accessible from `localhost`, where Kibana is installed.

Learning by doing is invaluable, so I recommend using the documentation and other resources to guide your setup. Experimenting and troubleshooting are essential parts of the process, just read, build, and fix until you get it right!

Here are some resources that I found particularly useful during the installation:

* [Official Elastic's Documentation](https://www.elastic.co/guide/en/elastic-stack/current/installing-stack-demo-self.html) 
* [Newton Paul’s Guide for Installing Elastic SIEM and Elastic EDR](https://newtonpaul.com/how-to-install-elastic-siem-and-elastic-edr/) (Note: This guide covers version 7.x; for version 8.x, expect a few differences, especially in the X-Pack configuration)
* [Elastic 8.x Installation Tutorial on Youtube by IppSec](https://www.youtube.com/watch?v=Ts-ofIVRMo4&t=150s) (A true lifesaver for tricky setup issues!)
  

### Notes
1. To test out Elasticsearch if its running properly or not, use the following command:
```bash
curl -k https://elastic:<elastic_password>@localhost:9200 #since i'm deploying on self-host with no trusted ca-certificates, -k flag used to bypass the certcheck
```
You should receive a response similar to this:
```json
{
  "name" : "node-1",
  "cluster_name" : "soc-in-my-pocket-cluster",
  "cluster_uuid" : "nUtIqn08RwudI_32tg-q8Q",
  "version" : {
    "number" : "8.15.3",
    "build_flavor" : "default",
    "build_type" : "deb",
    "build_hash" : "f97532e680b555c3a05e73a74c28afb666923018",
    "build_date" : "2024-10-09T22:08:00.328917561Z",
    "build_snapshot" : false,
    "lucene_version" : "9.11.1",
    "minimum_wire_compatibility_version" : "7.17.0",
    "minimum_index_compatibility_version" : "7.0.0"
  },
  "tagline" : "You Know, for Search"
}
```



2. Put NGINX in front of Kibana
   
     Well, i think this is the one of the best practices when deploying Elastic Stack, i just don't want to expose Kibana directly to the network.

     And also, putting NGINX in front of Kibana help me encrypt traffic between users to Kibana GUI (I will generate a self-signed certificate in the future) while keeping the Kibana connection unencrypted which is much easier for me to manage and integrate with other components in the project

4. Adding Elastic certificate into Fleet server
   
   You can move the elasticsearch certificate into Fleet via SCP, FTP or using ``python3 -m http.server`` on the server running Elasticsearch and then get the search on the Fleet server by using `curl <Server_running_Elasticsearch_IP_address>:8000/http_ca.crt -o /path/to/.crt`

   And add the `.crt` to Fleet Server Installation Guide
```bash
curl -L -O https://artifacts.elastic.co/downloads/beats/elastic-agent/elastic-agent-8.15.3-linux-x86_64.tar.gz
tar xzvf elastic-agent-8.15.3-linux-x86_64.tar.gz
cd elastic-agent-8.15.3-linux-x86_64
sudo ./elastic-agent install \
  --fleet-server-es=https://<Elastic_IP_Address>:9200 \
  --fleet-server-service-token=<Token> \
  --fleet-server-policy=fleet-server-policy \
  --fleet-server-es-ca-trusted-fingerprint=<fingerprint> \
  --fleet-server-port=8220 \ 
  --fleet-server-es-ca=/path/to/.crt \  #adding the path where elasticsearch certificate locate
  --insecure #Bypass Trusted CA-Certificates check since this my local project, don't recommend for production use                     
```



### Post installation 
1. After installation, go to http://<server_ip>
2. Login with your elastic account (username & password).
   
![Admin account setup](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/elasticsiem/elastic-login.png)

### Useful info / Important note
* Add `--insecure` to the end of elastic agent installation for bypass trusted ca-certificates check when you trying to install elastic agent on guest (Window / Linux Workstation, etc)

