import { Sequelize } from "sequelize";
import dotenv from "dotenv"

dotenv.config({path: '../secrets.development.env'})
const dbName = process.env.DB_NAME
const dbUser = process.env.DB_USER
const dbPassword = process.env.DB_PASSWORD

const db = new Sequelize(dbName, dbUser, dbPassword, {
    host: process.env.DB_HOST,
    port: process.env.DB_PORT,
    dialect: 'mysql',
    define: {
        timestamps: false
    },
    pool: {
        max: 5,
        min: 0,
        idle: 10000,
        acquire: 30000
    },
    operatorsAliases: false
})

export default db