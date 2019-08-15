import rpc_correlation as r
import sys

_, snd = sys.argv

def intTryParse(val):
    try:
        return int(val), True
    except ValueError:
        return val, False

integer, is_int = intTryParse(snd)

if is_int:
    r.client("{0} - id".format(integer), "{0} - message".format(integer)) # ususally uuid or guid
else:
    r.server()