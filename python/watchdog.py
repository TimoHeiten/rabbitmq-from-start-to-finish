import pika
import requests
from requests.auth import HTTPBasicAuth as basicAuth
import time

_status = "backing_queue_status"
_arguments = "arguments"
_mode = "mode"

url = "http://localhost:15672/api/queues/%2F/new_queue"

while (True):
    response = requests.get(url, auth=basicAuth("guest", "guest"))
    queue = response.json()

    try:
        is_lazy = False
        if _status in queue:
            is_lazy = queue[_status][_mode] == 'lazy'
        elif _arguments in queue:
            is_lazy = queue[_arguments][_mode] == 'lazy'

        if is_lazy:
            print("all is well!")
        else:
            raise Exception
    except:
        print("lazy mode not available")
    
    time.sleep(10)