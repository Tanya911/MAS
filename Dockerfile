FROM ubuntu:14.04

# Update packages
RUN apt-get update -y

# Install Mono
RUN apt-get install mono-complete -y
RUN apt-get update -y

# Install firefox
RUN apt-get update && apt-get install -y firefox

# Install ipython notebook
RUN apt-get -qq update && apt-get install --no-install-recommends -y libcurl4-openssl-dev libxml2-dev \
    apt-transport-https python-dev libc-dev pandoc python-pip pkg-config liblzma-dev libbz2-dev libpcre3-dev \
    build-essential libblas-dev liblapack-dev gfortran libzmq3-dev curl \
    libfreetype6-dev libpng-dev net-tools procps r-base libreadline-dev && \
    pip install distribute --upgrade

RUN pip install pyzmq
RUN pip install ipython==2.4 
RUN pip install jinja2 
RUN pip install tornado 
RUN pip install pygments 
RUN pip install numpy==1.9.0
RUN pip install biopython
RUN pip install scipy 
#RUN sudo apt-get install python-sklearn -y 
RUN pip install pandas==0.14.1
RUN pip install matplotlib==1.4.0
RUN pip install seaborn==0.5.1
    
RUN apt-get autoremove -y && apt-get clean && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

# Replace 1000 with your user / group id
RUN export uid=1000 gid=1000 && \
    mkdir -p /home/developer && \
    echo "developer:x:${uid}:${gid}:Developer,,,:/home/developer:/bin/bash" >> /etc/passwd && \
    echo "developer:x:${uid}:" >> /etc/group && \
    echo "developer ALL=(ALL) NOPASSWD: ALL" > /etc/sudoers.d/developer && \
    chmod 0440 /etc/sudoers.d/developer && \
    chown ${uid}:${gid} -R /home/developer

USER developer
ENV HOME /home/developer

# Bundle app source
RUN sudo mkdir /src/
RUN sudo mkdir /src/App
COPY src/App /src/App
COPY src/*.ipynb /src/
COPY src/*.sh /src/
RUN sudo chmod u+x /src/run.sh

# Build MAS app
RUN cd src; cd App; sudo xbuild /p:Configuration=Release MAS.csproj

# Expose
# IPython will run on port 8888, export this port to the host system
EXPOSE 8888

# Adding a Volume for the db
VOLUME ["/data"]