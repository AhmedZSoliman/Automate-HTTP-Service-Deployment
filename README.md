# Automate-HTTP-Service-Deployment on EC2 instance using bash scripting 
```bash

vim build.sh
```

```bash

 #!/bin/bash

sudo apt update
echo "Install dotnet"
sudo apt install -y aspnetcore-runtime-8.0
sudo apt install -y dotnet-sdk-8.0

#install git
echo "install git"
apt install -y git
 
#clone repo from github 
cd /home/ubuntu
echo "git clone"
git clone https://github.com/AhmedZSoliman/Automate-HTTP-Service-Deployment-on-EC2-Instance.git
mv Automate-HTTP-Service-Deployment-on-EC2-Instance srv-02
cd srv-02

#build the dot net service
echo "dotnet build"
echo 'DOTNET_CLI_HOME=/tmp' >> /etc/environment
export DOTNET_CLI_HOME=/tmp

dotnet publish -c Release --self-contained=false --runtime linux-x64


cat >/etc/systemd/system/srv-02.service <<EOL
[Unit]
Description=Dotnet S3 info service

[Service]
ExecStart=/usr/bin/dotnet /home/ubuntu/srv-02/bin/Release/net8.0/linux-x64/publish/srv02.dll
SyslogIdentifier=srv-02

Environment=DOTNET_CLI_HOME=/tmp

[Install]
WantedBy=multi-user.target
EOL

systemctl daemon-reload

#run it
systemctl start srv-02

```

