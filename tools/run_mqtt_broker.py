"""ReLight-X source file.

Project: ReLight-X
Developer: Amin Zoroufi
Role: AI Researcher / XR Developer
Location: Dubai, UAE
Contact: aminn.zoroufi@gmail.com
Usage: part of the ReLight-X digital twin, adaptive-lighting simulation, board testing, or validation toolchain.
"""

from __future__ import annotations

import asyncio
import signal

from amqtt.broker import Broker


CONFIG = {
    "listeners": {
        "default": {
            "type": "tcp",
            "bind": "127.0.0.1:1883",
        }
    },
    "sys_interval": 10,
    "topic-check": {"enabled": False},
    "auth": {"allow-anonymous": True},
}


async def main() -> None:
    broker = Broker(CONFIG)
    stop_event = asyncio.Event()

    def request_stop() -> None:
        stop_event.set()

    loop = asyncio.get_running_loop()
    for sig in (signal.SIGINT, signal.SIGTERM):
        loop.add_signal_handler(sig, request_stop)

    await broker.start()
    print("ReLight-X local MQTT broker running at mqtt://127.0.0.1:1883")
    print("Press Ctrl+C to stop.")
    await stop_event.wait()
    await broker.shutdown()


if __name__ == "__main__":
    asyncio.run(main())
