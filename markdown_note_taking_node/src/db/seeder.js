import Note from "../models/Note.js"
import db from "./config.js"
import notes from "./data/notes.js"
import { exit } from "process"

const importData = async () => {
    try{
        await db.authenticate()
        await db.sync()

        await Note.bulkCreate(notes)

        console.log('Data imported')
        exit(1)

    }catch(e){
        console.log(`Error: ${e}`)
    }
}

const deleteData = async () => {
    try{
        await db.authenticate()
        await db.sync()

        await Note.destroy({where: {}, truncate: true})

        console.log('Data deleted')
        exit(1)

    }catch(e){
        console.log(`Error: ${e}`)
    }
}

if(process.argv[2] === '--i'){
    importData()
}

if(process.argv[2] === '--d'){
    deleteData()
}