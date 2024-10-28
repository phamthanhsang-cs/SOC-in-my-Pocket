<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/opencti-logo.png alt="logo">


OpenCTI Installation Notes

</h1>

OpenCTI is a resource-intensive platform due to the many components required for its operation, including Elasticsearch, Redis, MinIO, RabbitMQ, OpenCTI-core, and others. The more **connectors** you use to feed threat data into OpenCTI, the more resource-demanding the platform becomes.

To address these requirements, I’ve set up OpenCTI on a **two-node Debian cluster** using Docker, with **Docker Swarm** for orchestration. The nodes are managed through **Portainer**, which simplifies monitoring and scaling. If I encounter storage limitations or exceed the hardware resources like RAM or CPU, or if I need additional redundancy, I can easily add more nodes to the cluster and join them to the Docker Swarm.

More information about OpenCTI installation: 

### Prerequisites
1. **Docker**, you can find install instructions [here](https://docs.docker.com/engine/install/debian/) for Debian
2. **Docker Swarm**, to get more details how to create and join a swarm, go to [Docker's documentation](https://docs.docker.com/engine/swarm/swarm-tutorial/create-swarm/) for Swarm Mode
3. **Docker Portainer**, a very lightweight node and easy to install/manage, you can find it out [here](https://docs.portainer.io/start/install-ce/server/docker/linux)


### Troubleshooting
While deploying the OpenCTI Stack, I encountered an issue with the 'health check' feature that prevented the stack from starting. Seems like this issue appear due to a change in the docker-compose.yml file in the latest version of [OpenCTI](https://github.com/OpenCTI-Platform/docker) (6.3.5) Docker.


1. Example of provided docker-compose.yml file
```bash
```

2. Example of provided docker-compose.yml yml
```bash
```

3. Run the stack.
```bash
```


   
![Admin account setup]()

### Useful info / Important note
* Check out [opencti's documentation](https://docs.opencti.io/latest/) for more information
* Since MinIO using port 9000 for communication, you have to port-forwarding the Portainer default port to others, mine is 19000:9000


