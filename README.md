# Automate-HTTP-Service-Deployment on EC2 instance 
```bash

vim build.sh
```

```bash

#!/bin/bash
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt update

 sudo apt install -y software-properties-common
sudo add-apt-repository universe -y

sudo apt update

echo "Install dotnet"
sudo apt install -y aspnetcore-runtime-6.0
sudo apt install -y dotnet-sdk-6.0

#install git
echo "install git"
apt install git
 
#clone repo from github 
cd /home/ubuntu
echo "git clone"
git clone  https://github.com/AhmedZSoliman/Automate-HTTP-Service-Deployment-on-EC2-Instance.git
mv Automate-HTTP-Service-Deployment-on-EC2-Instance  srv-02
cd srv-02

#build the dot net service
echo "dotnet build"
echo 'DOTNET_CLI_HOME=/temp' >> /etc/environment
export DOTNET_CLI_HOME=/temp
dotnet publish -c Release --self-contained=false --runtime linux-x64


cat >/etc/systemd/system/srv-02.service <<EOL
[Unit]
Description=Dotnet S3 info service

[Service]
ExecStart=/usr/bin/dotnet /home/ubuntu/srv-02/bin/Release/netcoreapp6/linux-x64/srv02.dll
SyslogIdentifier=srv-02

Environment=DOTNET_CLI_HOME=/temp

[Install]
WantedBy=multi-user.target
EOL

systemctl daemon-reload

#run it
systemctl start srv-02

```

