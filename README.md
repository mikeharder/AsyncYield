# AsyncYield

## .NET
```
Sleep
[0.0003522] Before task1
[1.0168129] Before task2
[2.017457] Before await
[2.017839] After await

YieldThenSleep
[1.5E-06] Before task1
[0.0012092] Before task2
[0.0013622] Before await
[1.0144027] After await

TaskRunSleep
[1.5E-06] Before task1
[0.0003455] Before task2
[0.0005491] Before await
[1.0156869] After await
```

## Python
```
sleep
[0.0] Before task1
[0.0009984970092773438] Before task2
[0.0009984970092773438] Before await
[2.0343027114868164] After await

await_sleep
[0.0] Before task1
[0.0] Before task2
[0.0] Before await
[2.0175182819366455] After await

create_task_sleep
[0.0] Before task1
[0.00048232078552246094] Before task2
[0.00048232078552246094] Before await
[2.0284652709960938] After await

asyncio_sleep
[0.0] Before task1
[0.0] Before task2
[0.0] Before await
[1.0156066417694092] After await
```
