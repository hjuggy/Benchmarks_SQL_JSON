This example demonstrates how you can shorten the execution time of queries against the database (for example  PostgreSQL).
Used by:
	- EF
	- Dapper
	- Dapper + JSON Functions and Operators (https://www.postgresql.org/docs/9.4/functions-json.html)
	- Dapper + JSON Functions and Operators (with separated repository for each entity)

As you can see from the table, using JSON Functions and Operators when working with collections significantly increases the performance of the application.


|                      Method |     Mean |     Error |    StdDev |   Median | Gen 0 | Gen 1 | Gen 2 | Allocated | Completed Work Items | Lock Contentions |
|---------------------------- |---------:|----------:|----------:|---------:|------:|------:|------:|----------:|---------------------:|-----------------:|
|                  CreatUseEF | 9.952 ms | 1.6265 ms | 4.7959 ms | 9.103 ms |     - |     - |     - |  3,827 KB |               4.0000 |                - |
|              CreatUseDapper | 6.584 ms | 0.5591 ms | 1.6486 ms | 6.305 ms |     - |     - |     - |    189 KB |              22.0000 |                - |
| CreatUseDapperJsonSeparated | 2.798 ms | 0.5964 ms | 1.7584 ms | 2.314 ms |     - |     - |     - |     42 KB |               4.0000 |                - |
|          CreatUseDapperJson | 2.248 ms | 0.1500 ms | 0.4423 ms | 2.132 ms |     - |     - |     - |     37 KB |               2.0000 |                - |
