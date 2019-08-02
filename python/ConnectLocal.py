import pika

def _create_channel(conn_params):
    connection = pika.BlockingConnection(conn_params)
    # get channel
    channel = connection.channel()
    return channel

def do_connect():
    credentials = pika.PlainCredentials("guest", "guest")
    conn_param = pika.connection.ConnectionParameters(credentials=credentials)

    # establish tcp connection
    return _create_channel(conn_param)

def connect_heartbeat():
    credentials = pika.PlainCredentials("guest", "guest")
    conn_param = pika.connection.ConnectionParameters(heartbeat=1500, credentials=credentials)

    # establish tcp connection
    return _create_channel(conn_param)