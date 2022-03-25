import asyncio
import time

async def main():
    await test(sleep, "sleep")
    await test(await_sleep, "await_sleep")
    await test(lambda : asyncio.create_task(sleep()), "create_task_sleep")
    await test(asyncio_sleep, "asyncio_sleep")

async def test(func, description):
    print(description)

    start_time = time.time()

    log("Before task1", start_time)
    task1 = func()

    log("Before task2", start_time)
    task2 = func()

    log("Before await", start_time)
    await asyncio.gather(task1, task2)
    log("After await", start_time)
    print()


async def sleep():
    time.sleep(1)

async def await_sleep():
    await asyncio.sleep(0)
    time.sleep(1)

async def asyncio_sleep():
    await asyncio.sleep(1)

def log(message, start_time):
    print(f'[{time.time() - start_time}] {message}')

asyncio.run(main())
