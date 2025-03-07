import express from 'express'
import notesRouter from './src/routes/notes.js'
import db from './src/db/config.js'

const app = express()

try{
	await db.authenticate().
	db.sync()
	console.log('Database connected')
}catch(e){
	console.log(`Something went wrong: ${e}`)
}

app.use('/notes', notesRouter)

app.listen(3000, () => {
	console.log('App listenning on port 3000')
})
