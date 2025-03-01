import express from 'express'
import notesRouter from './src/routes/notes.js'
import db from './src/db/config.js'

const app = express()

db.authenticate()
	.then(() => {
		console.log('Connection has been established successfully.')
	})
	.catch((error) => {
		console.error('Unable to connect to the database:', error)
	})

app.use('/notes', notesRouter)

app.listen(3000, () => {
	console.log('App listenning on port 3000')
})
