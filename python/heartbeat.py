import ConnectLocal
from requests import get
from requests.auth import HTTPBasicAuth as auth


#aliveness
def check_alive():
    creds = auth('guest','guest')
    url = "http://localhost:15672/api/aliveness-test/%2F"
    response = get(url, auth=creds)

    status = response.json()

    print("aliveness test returned with '{0}'".format(response.status_code))
    print(status)


#tcp
def connect():
    return ConnectLocal.connect_heartbeat()

channel = connect()
while (True):
    try:
        check_alive()
        channel.basic_publish("", "pika_queue", body="aliveness")
    except:
        print("not alive -- action is required")
    
    v = input("press ctrl+c to stop")