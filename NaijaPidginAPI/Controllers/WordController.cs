using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaijaPidginAPI.DTOs;
using NaijaPidginAPI.Entities;
using NaijaPidginAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WordController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWord(AddWordDTO wordDTO)
        {
            var word = mapper.Map<Word>(wordDTO);
            unitOfWork.WordRepository.AddWord(word);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetWords()
        {
            var words = await unitOfWork.WordRepository.GetWordsAync();
            var wordDTO = mapper.Map<IEnumerable<ListWordDTO>>(words);
            return Ok(wordDTO);
        }

        [HttpGet("list/{id}")]
        public async Task<IActionResult> GetWordsById(int id)
        {
            var word = await unitOfWork.WordRepository.GetWordByIdAsync(id);
            var wordDTO = mapper.Map<ListWordDTO>(word);
            return Ok(wordDTO);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateWord(int id, AddWordDTO wordDTO)
        {
            var word = await unitOfWork.WordRepository.GetWordByIdAsync(id);
            if (word == null)
                return NotFound();

            word.LastUpdatedOn = DateTime.Now;
            mapper.Map(wordDTO, word);
            await unitOfWork.SaveAsync();
            return StatusCode(200);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            unitOfWork.WordRepository.DeleteWord(id);
            await unitOfWork.SaveAsync();
            return Ok("Word has been deleted successfully");
        }

    }
}
