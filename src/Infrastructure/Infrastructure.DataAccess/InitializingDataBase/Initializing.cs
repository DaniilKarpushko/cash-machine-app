using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Infrastructure.DataAccess.InitializingDataBase;

[Migration(1, "Initial")]
public class Initializing : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return """
               create type operation_type as enum(
                    'replenishment',
                    'withdraw');

               create table accounts(
                    account_id int primary key,
                    password varchar(6),
                    balance decimal);

               create table operations(
                    operation_id int primary key generated always as identity,
                    account_id int,
                    operation operation_type,
                    date date,
                    amount decimal);
                    
               insert into accounts(account_id, password, balance)
               values (1, 'abcdef', 1);
               """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return """
               drop table accounts;
               drop table operations;
               """;
    }
}