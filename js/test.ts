
async function main() {
    await test(sleep, "Sleep");
    await test(delay, "Delay");
    await test(yieldThenSleep, "YieldThenSleep");
    await test(noopLoop, "NoopLoop");
}

async function test(func: () => Promise<void>, description: string) {
    console.log(description);

    const startTime = new Date().getTime();

    log("Before task1", startTime);
    const task1 = func();

    log("Before task2", startTime);
    const task2 = func();

    log("Before await", startTime);
    await Promise.all([task1, task2]);
    log("After await", startTime);

    console.log();
}

async function sleep() {
    Atomics.wait(new Int32Array(new SharedArrayBuffer(4)), 0, 0, 1000);
}

async function delay() {
    await new Promise(f => setTimeout(f, 1000));
}

async function yieldThenSleep() {
    await new Promise(f => setTimeout(f, 0));
    Atomics.wait(new Int32Array(new SharedArrayBuffer(4)), 0, 0, 1000);
}

async function noopLoop() {
    const startTime = new Date().getTime();
    console.log(`startTime: ${startTime}`);
    while (new Date().getTime() - startTime < 1000) {
        await noop();
    }
}

async function noop() {
}

function log(message: string, startTime: number) {
    console.log(`[${(new Date().getTime() - startTime) / 1000}] ${message}`);
}

main();