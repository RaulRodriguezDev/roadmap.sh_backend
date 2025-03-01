import express from 'express'
import notesRouter from './src/routes/notes.js'

const app = express()

app.use('/notes', notesRouter)

app.listen(3000, () => {
	console.log('App listenning on port 3000')
})
